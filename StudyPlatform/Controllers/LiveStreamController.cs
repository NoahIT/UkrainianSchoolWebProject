using System.ComponentModel.DataAnnotations;
using BL.Auth;
using BL.Orders;
using BL.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using StudyPlatform.ViewModels;

namespace StudyPlatform.Controllers
{
    public class LiveStreamController : Controller
    {
        private readonly IUserService userService;
        private readonly IOrder order;
        private readonly IStreamService streamService;
        private readonly IJwtTokenGenerator jwtTokenGenerator;

        public LiveStreamController(IUserService userService, IOrder order, IStreamService streamService, IJwtTokenGenerator jwtTokenGenerator)
        {
            this.userService = userService;
            this.order = order;
            this.streamService = streamService;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpGet]
        [Route("/live-stream")]
        public async Task<IActionResult> LiveStream([Required] int s)
        {
            var userId = await userService.GetUserId();

            if (userId == null)
            {
                return NotFound();
            }

            var sIds = await order.GetApprovedOrdersSubjectIds((int)userId);

            if (sIds == null || !sIds.Contains(s))
            {
                return Redirect("/");
            }

            var stream = await streamService.GetStreamBySubjectId(s);

            if (stream == null)
            {
                return NotFound();
            }

            string fullName = await userService.GetFullNameAndUserName((int)userId);

            var token = jwtTokenGenerator.GenerateJwtToken((int)userId);

            Response.Cookies.Append("jwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,    
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(30)
            });

            return View("LiveStream", new StreamViewModel()
            {
                Stream = stream,
                FullName = fullName
            });
        }
    }
}
