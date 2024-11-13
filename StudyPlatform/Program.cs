using BL.Auth;
using BL.Helpers;
using BL.Helpers.Interfaces;
using DAL;
using DAL.Helpers;
using DAL.Implementations;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using BL.Coupons;
using BL.Email;
using BL.Orders;
using BL.Recovery;
using BL.ScheduleF;
using BL.Services;
using BL.Subscriptions;
using DAL.Coupons;
using DAL.Email;
using DAL.Orders;
using DAL.Schedule;
using DAL.ScheduleF;
using DAL.Streams;
using DAL.Subjects;
using DAL.Subscriptions;
using DAL.Subscriptions.Payments;
using DAL.UserManager;
using StudyPlatform.Services;

namespace StudyPlatform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            var secretKey = builder.Configuration["ApiSetting:secret"];

            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

            builder.Services.AddTransient<IEmailHelper, EmailHelper>();
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<PlatformDbContext>(options =>
                options.UseMySQL(builder.Configuration.GetConnectionString("Prod") ?? ""));


            // BL
            builder.Services.AddScoped<IWebCookie,WebCookie>();
            builder.Services.AddScoped<IAuth, Auth>();
            builder.Services.AddScoped<ICurrentUser, CurrentUser>();
            builder.Services.AddScoped<IEncrypt, Encrypt>();
            builder.Services.AddScoped<IDbSession, DbSession>();
            builder.Services.AddScoped<IRecovery, RecoveryPassword>();
            builder.Services.AddTransient<INotificationService, NotificationService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IScheduleBL, ScheduleBL>();
            builder.Services.AddScoped<ISubscription, Subscription>();
            builder.Services.AddScoped<ITransactionHelper, TransactionHelper>();
            builder.Services.AddScoped<IOrder, OrderBL>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ILessonsService, LessonsService>();
            builder.Services.AddScoped<ITeacherService, TeacherService>();
            builder.Services.AddScoped<IStreamService, StreamService>();
            builder.Services.AddScoped<ICouponService, CouponService>();
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();


            // DAL
            builder.Services.AddScoped<IDbSessionDAL, DbSessionDAL>();
            builder.Services.AddScoped<ILessonsDAL, LessonsDAL>();
            builder.Services.AddScoped<ITeacherDAL, TeacherDAL>();
            builder.Services.AddScoped<IRecoveryPasswordDAL, RecoveryPasswordDAL>();
            builder.Services.AddTransient<IEmailSenderDAL, EmailSenderDAL>();
            builder.Services.AddScoped<IAuthDAL, AuthDAL>();
            builder.Services.AddScoped<IScheduleDAL, ScheduleDAL>();
            builder.Services.AddScoped<ISubscriptionDAL, SubscriptionDAL>();
            builder.Services.AddScoped<IOrderDAL,OrderDAL>();
            builder.Services.AddScoped<IUserDAL, UserDAL>();
            builder.Services.AddScoped<IPaymentDAL, PaymentDAL>();
            builder.Services.AddScoped<ISubjectDAL, SubjectDAL>();
            builder.Services.AddScoped<IStreamDAL, StreamDAL>();
            builder.Services.AddScoped<ICouponDAL, CouponDAL>();
            builder.Services.AddScoped<ISignatureGenerator>(provider => new SignatureGenerator(secretKey));

            builder.Services.AddHostedService<SubscriptionCheckService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error404");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
