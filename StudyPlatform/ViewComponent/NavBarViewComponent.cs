using BL.Auth;
using BL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace StudyPlatform.ViewComponent
{
    public class NavBarViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {

        private readonly ICurrentUser currentUser;
        private readonly IUserService userService;
        public NavBarViewComponent(ICurrentUser current, IUserService userService)
        {
            this.currentUser = current;
            this.userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await userService.GetAccessModel();

            ViewBag.Login = (await userService.GetHelloUserLoginAndSubsValid()).Login ?? "default_username";

            var helloUserModel = await userService.GetHelloUserModel();

            var uniqueGradeClasses = helloUserModel?.ApprovedOrders?
                .SelectMany(order => order.Subjects)
                .Select(subject => subject.Subject.GradeClass) 
                .Distinct()
                .ToList();

            var parsed = uniqueGradeClasses?.Select(x => (int)x).ToList();

            ViewBag.Class = string.Join(", ", parsed ?? new List<int>());

            return View("NavBar", model);
        }

    }
}
