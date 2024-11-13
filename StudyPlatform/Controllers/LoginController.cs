using Microsoft.AspNetCore.Mvc;
using StudyPlatform.Models;
using System.Diagnostics;
using BL.Auth;
using BL.Exceptions;
using BL.Recovery;
using StudyPlatform.ViewModels;
using StudyPlatform.Middleware;

namespace StudyPlatform.Controllers
{
    
    public class LoginController : Controller
    {
        private readonly IAuth auth;
        private readonly IRecovery recovery;

        public LoginController(IAuth auth, IRecovery recovery)
        {
            this.auth = auth;
            this.recovery = recovery;
        }

        [SiteNotAuthorize()]
        [HttpGet]
        [Route("/login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [SiteNotAuthorize()]
        [HttpPost]
        [Route("/login-account")]
        public async Task<IActionResult> LoginAccount(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                       int id = await auth.Authenticate(model.Email, model.Password);

                       if (id > 0)
                       {
                           return Redirect("/");
                       }

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Password", "Не правильно введен пароль или почта!");
                }
            }

            return View("Login", model);
        }

        [SiteAuthorize()]
        [HttpGet]
        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await auth.Logout();

            return Redirect("/");
        }

        //[SiteAuthorize()]
        [HttpGet]
        [Route("/EmailToPasswordRecovery")]
        public IActionResult EmailToPasswordRecovery()
        {
            return View("EmailToPasswordRecovery");
        }

        //[SiteAuthorize()]
        [HttpPost]
        [Route("/EmailToPasswordRecoveryMessage")]
        public async Task<IActionResult> EmailToPasswordRecoveryMessage(string email)
        {
            await recovery.SendToEmailPassVerifLink(email);

            return View("EmailToPasswordRecoveryMessage", email);
        }

        //[SiteAuthorize()]
        [HttpGet]
        [Route("/password-recovery")]
        public IActionResult PasswordRecovery(RecoveryViewModel model)
        {
            var email = recovery.GetEmailByCode(model.Code);

            return View("PasswordRecovery", model);
        }

        //[SiteAuthorize()]
        [HttpPost]
        [Route("/password-recovery")]
        public async Task<IActionResult> PasswordRecoveryChanging(RecoveryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await recovery.ChangePassword(model.Password1, model.Password2, model.Code);

                    return Redirect("/");
                }
                catch (PasswordRepeatException e)
                {
                    Console.WriteLine(e);

                    ModelState.AddModelError("password2", "Пароли не совпадают!");
                }
                catch (EasyPasswordException e)
                {
                    Console.WriteLine(e);

                    ModelState.AddModelError("password2", "Пароль должен содержать хотя бы 5 символов!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return View("Error404");
                }
            }

            return View("PasswordRecovery", model);
        }
    }
}
