using Microsoft.AspNetCore.Mvc;
using StudyPlatform.Models;
using System.Diagnostics;
using BL.Coupons;
using BL.Services;
using DAL.Models;
using StudyPlatform.Middleware;
using StudyPlatform.ViewModels;

namespace StudyPlatform.Controllers
{
    [SiteAuthorize()]
    public class ApplyCouponController : Controller
    {
        private readonly IUserService userService;
        private readonly ICouponService couponService;

        public ApplyCouponController(IUserService userService, ICouponService couponService)
        {
            this.userService = userService;
            this.couponService = couponService;
        }

        [HttpGet]
        [Route("/applyCoupon")]
        public async Task<IActionResult> ApplyCoupon(bool? model)
        {
            if (model!= null && (bool)model)
            {
                TempData["isSuccess"] = false;

                return View("ApplyCoupon", new ApplyCouponViewModel()
                {
                    ActiveCoupon = null
                });
            }

            var userId = await userService.GetUserId();

            if (userId == null)
            {
                return View("ApplyCoupon", new ApplyCouponViewModel()
                {
                    ActiveCoupon = null
                });
            }


            var activeCoupon = await couponService.FindActiveCoupon((int)userId);
            
            return View("ApplyCoupon",new ApplyCouponViewModel()
            {
                ActiveCoupon = activeCoupon
            });
        }

        [HttpPost]
        [Route("/apply-coupon")]
        public async Task<IActionResult> ApplyCouponPost(string code)
        {
            var userId = await userService.GetUserId();

            if (userId == null)
            {
                return Redirect("/");
            }

            var r = await couponService.ApplyCoupon(code, (int)userId);

            if (!r)
            {
                return RedirectToAction("ApplyCoupon", new {model = true});
            }

            return Redirect("/applyCoupon");
        }
    }
}
