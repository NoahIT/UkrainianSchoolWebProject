using LiveStreamingServerNet.Networking.Helpers;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using System.Net;
using System.Reflection;
using System.Text;
using LiveStreamingServerNet;
using LiveStreamingServerNet.StreamProcessor.Contracts;
using LiveStreamingServerNet.StreamProcessor.Installer;
using LiveStreamingServerNet.Utilities.Contracts;
using LiveStreamingServerNet.StreamProcessor.Hls.Contracts;
using LiveStreamingServerNet.StreamProcessor.AspNetCore.Configurations;
using LiveStreamingServerNet.StreamProcessor.AspNetCore.Installer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using StreamServer.Database;
using StreamServer.Database.Models;
using StreamServer.Database.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Web;
using LiveStreamingServerNet.Utilities.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using LiveStreamingServerNet.Networking.Contracts;
using LiveStreamingServerNet.Rtmp.Auth;
using LiveStreamingServerNet.Rtmp.Auth.Contracts;
using static StreamServer.Program.DemoAuthorizationHandler;


namespace StreamServer
{
    public class Program
    {

        public static async Task Main(string[] args)
        {

            var outputDir = Path.Combine(Directory.GetCurrentDirectory(), "hls-output");
            new DirectoryInfo(outputDir).Create();

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<StreamDbContext>(options =>
                options.UseMySQL(builder.Configuration.GetConnectionString("Prod") ?? ""));


            var liveStreamingServer = LiveStreamingServerBuilder.Create()
                .ConfigureRtmpServer(options =>
                {
                    options.Services.AddDbContext<StreamDbContext>(options =>
                        options.UseMySQL(builder.Configuration.GetConnectionString("Prod") ?? ""));


                    options.Services.AddTransient<IStreamService, StreamService>();
                    options.Services.AddTransient<IStreamProcessorEventHandler, HlsTransmuxerEventListener>();

                    var streamProcessingBuilder = options.AddStreamProcessor(options =>
                        options.AddStreamProcessorEventHandler<HlsTransmuxerEventListener>()
                    );

                    streamProcessingBuilder.AddHlsTransmuxer(options =>
                    {
                        options.OutputPathResolver = new HlsTransmuxerOutputPathResolver(outputDir);
                        options.MinSegmentLength = TimeSpan.FromSeconds(1);
                        options.MaxSegmentSize = 5000000;
                        options.SegmentListSize = 1;
                        options.DeleteOutdatedSegments = true;
                    });

                    options.Services.AddSingleton<IPasswordValidator, DemoPasswordValidator>();
                    options.AddAuthorizationHandler<DemoAuthorizationHandler>();
                })
                .ConfigureLogging(options => options.AddConsole())
                .Build();


            builder.Services.AddBackgroundServer(liveStreamingServer, new IPEndPoint(IPAddress.Any, 1935));

            builder.Services.AddCors(options =>
                options.AddDefaultPolicy(policy =>
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("https://uniqum.school")
                )
            );
            var _configuration = builder.Configuration;

            var app = builder.Build();

            app.UseCors();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.StartsWithSegments("/hls"))
                {
                    var token = context.Request.Cookies["jwtToken"];
                    if (string.IsNullOrEmpty(token))
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Unauthorized");
                        return;
                    }

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

                    try
                    {
                        tokenHandler.ValidateToken(token, new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = true,
                            ValidIssuer = _configuration["Jwt:Issuer"],
                            ValidateAudience = true,
                            ValidAudience = _configuration["Jwt:Audience"],
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero
                        }, out SecurityToken validatedToken);

                        await next();
                    }
                    catch (Exception)
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Unauthorized");
                    }
                }
                else
                {
                    await next();
                }
            });

            app.UseHlsFiles(liveStreamingServer, new HlsServingOptions
            {
                Root = outputDir,
                RequestPath = "/hls"
            });

            //// https://localhost:5001/hls/live/demo/output.m3u8
            //// https://uniqum.school/streams/hls/live/demo/output.m3u8

            await app.RunAsync();
        }

        public interface IPasswordValidator
        {
            Task<bool> ValidatePassword(string password, int sId);
        }

        public class DemoPasswordValidator : IPasswordValidator
        {
            private readonly IStreamService _streamService;

            public DemoPasswordValidator(IStreamService streamService)
            {
                _streamService = streamService;
            }

            public async Task<bool> ValidatePassword(string password, int sId)
            {
                return await _streamService.ValidateTeacherCreditials(sId, password);
            }
        }

        public class DemoAuthorizationHandler : IAuthorizationHandler
        {
            private readonly IPasswordValidator _passwordValidator;

            public DemoAuthorizationHandler(IPasswordValidator passwordValidator)
            {
                _passwordValidator = passwordValidator;
            }

            public async Task<AuthorizationResult> AuthorizePublishingAsync(
                IClientInfo client,
                string streamPath,
                IReadOnlyDictionary<string, string> streamArguments,
                string publishingType)
            {
                // Accepting only the publishing path that includes a valid password parameter
                // For example: rtmp://127.0.0.1:1935/live/stream?password=123456

                if (streamArguments.TryGetValue("password", out var password) &&
                    streamArguments.TryGetValue("sId", out var sId) &&
                    int.TryParse(sId, out int sIds) &&
                    await _passwordValidator.ValidatePassword(password, sIds))
                {
                    return AuthorizationResult.Authorized();
                }

                return AuthorizationResult.Unauthorized("incorrect password");
            }

            public Task<AuthorizationResult> AuthorizeSubscribingAsync(
                IClientInfo client,
                string streamPath,
                IReadOnlyDictionary<string, string> streamArguments)
            {
                return Task.FromResult(AuthorizationResult.Authorized());
            }


        }
        public class HlsTransmuxerOutputPathResolver : IHlsOutputPathResolver
        {
            private readonly string _outputDir;

            public HlsTransmuxerOutputPathResolver(string outputDir)
            {
                _outputDir = outputDir;
            }

            public ValueTask<string> ResolveOutputPath(IServiceProvider services, Guid contextIdentifier,
                string streamPath, IReadOnlyDictionary<string, string> streamArguments)
            {
                return ValueTask.FromResult(Path.Combine(_outputDir, streamPath.Split('/').Last(), "output.m3u8"));
            }
        }

        public class HlsTransmuxerEventListener : IStreamProcessorEventHandler
        {
            private readonly ILogger _logger;
            private readonly IStreamService _streamService;

            public HlsTransmuxerEventListener(ILogger<HlsTransmuxerEventListener> logger, IStreamService streamService)
            {
                _logger = logger;
                _streamService = streamService;
            }


            public async Task OnStreamProcessorStartedAsync(IEventContext context,
                string transmuxer,
                Guid identifier,
                uint clientId,
                string inputPath,
                string outputPath,
                string streamPath,
                IReadOnlyDictionary<string, string> streamArguments)
            {
                try
                {
                    if (int.TryParse(streamPath.Split('-').Last(), out int sId))
                    {
                        var abbrName = streamPath.Split('/').Last();
                        await _streamService.StreamStarted(new StreamInformation()
                        {
                            Identifier = identifier,
                            AbbrName = abbrName,
                            StreamPath = "https://uniqum.school/streams/hls/live/" + abbrName + "/output.m3u8",
                            IsLive = true,
                            SubjectId = sId
                        });

                        _logger.LogInformation(
                            $"[{identifier}] Transmuxer {transmuxer} started: {inputPath} -> {outputPath}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error while processing stream start for {identifier}");
                }
                //streamPath : "/live/language-11"
            }

            public async Task OnStreamProcessorStoppedAsync(IEventContext context,
                string transmuxer,
                Guid identifier,
                uint clientId,
                string inputPath,
                string outputPath,
                string streamPath,
                IReadOnlyDictionary<string, string> streamArguments)
            {
                _logger.LogInformation(
                    $"[{identifier}] Transmuxer {transmuxer} stopped: {inputPath} -> {outputPath}");

                try
                {

                    if (int.TryParse(streamPath.Split('-').Last(), out int sId))
                    {
                        var abbrName = streamPath.Split('/').Last();
                        await _streamService.StreamEnded(new StreamInformation()
                        {
                            Identifier = identifier,
                            AbbrName = abbrName,
                            StreamPath = "https://uniqum.school/streams/hls/live/" + abbrName + "/output.m3u8",
                            IsLive = false,
                            SubjectId = sId
                        });
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error while processing stream start for {identifier}");
                }
            }
        }
    }
}
