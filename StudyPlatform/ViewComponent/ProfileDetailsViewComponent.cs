using BL.Auth;
using BL.Helpers.Interfaces;
using BL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using StudyPlatform.ViewModels;

namespace StudyPlatform.ViewComponent
{
    public class ProfileDetailsViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly IUserService _userService;
        private readonly IWebCookie webCookie;

        public ProfileDetailsViewComponent(IUserService userService, IWebCookie webCookie)
        {
            _userService = userService;
            this.webCookie = webCookie;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cookie = webCookie.Get(AuthConstants.SessionCookieName);

            var model = await _userService.GetAccount(cookie);

            var viewModel = new ProfileViewModel();

            if (model != null)
            {
                var userClassesNumbers = await _userService.GetClassNumberByApprovedOrders(model.Id);

                viewModel = new ProfileViewModel()
                {
                    FullName = model.FirstName + " " + model.LastName,
                    ClassNumber = userClassesNumbers,
                    Email = model.AppUser.Email,
                    Login = model.AppUser.Login ?? "",
                    Phone = model.AppUser.Phone ?? "",
                    UserId = model.Id
                };
            }
            return View("ProfileDetails", viewModel);
        }
    }
}
