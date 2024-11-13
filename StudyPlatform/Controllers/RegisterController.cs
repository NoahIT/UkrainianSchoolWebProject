using System.Diagnostics;
using BL.Auth;
using BL.Exceptions;
using BL.Helpers;
using BL.Helpers.Interfaces;
using BL.Orders;
using BL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using StudyPlatform.ViewModels;

namespace StudyPlatform.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAuth auth;
        private readonly IOrder order;
        private readonly ITransactionHelper transactionHelper;
        private readonly IUserService userService;

        public RegisterController(IAuth auth, IOrder order, ITransactionHelper transactionHelper, IUserService userService)
        {
            this.auth = auth;
            this.order = order;
            this.transactionHelper = transactionHelper;
            this.userService = userService;
        }


        [HttpPost]
        [Route("/register")]
        [ValidateAntiForgeryToken]
        public IActionResult Register(SelectingPlanViewModel planModel)
        {
            var viewModel = new RegisterViewModel()
            {
                GradeClass = planModel.GradeClass.Split(' ')[0],
                SubscriptionId = planModel.SubscriptionId,
                SubjectsId = planModel.SubjectsId
            };

            return View("Register", viewModel);
        }

        [HttpPost]
        [Route("/register-account")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAccount(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<int>? subjectsIdList = HelpersM.StringToListInt(model.SubjectsId);

                    if (subjectsIdList == null || model.GradeClass == null)
                    {
                        return Redirect("/selectPlan");
                    }

                    int id = 0;

                    var user = await userService.GetUser();

                    if (user != null)
                    {
                        id = user.Id;
                    }
                    else
                    {
                        id = await auth.RegisterNoAuth(new User()
                        {
                            AppUser = new AppUser()
                            {
                                Email = model.Email,
                                FirstPassword = Guid.NewGuid().ToString()
                            }
                        });
                    }

                    var orderId = await order.CreateOrder(model.GradeClass, id, model.SubscriptionId, subjectsIdList);

                    return Redirect($"/payment-redirect?orderId={orderId}");
                }
                catch (DuplicateEmailException e)
                {
                    Debug.WriteLine(e);
                    ModelState.TryAddModelError("Email", "Эл.Почта уже существует! Либо же зайдите в свой аккаунт.");
                }
                catch (EasyPasswordException easy)
                {
                    Debug.WriteLine(easy);
                    ModelState.TryAddModelError("Password", "Пароль должен содержать хотя бы из 5 символов!");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    ModelState.TryAddModelError("Password", "Произошла ошибка при проверке.");
                }
            }

            return View("Register", model);
        }
    }
}
