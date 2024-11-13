using BL.Services;
using Microsoft.AspNetCore.Mvc;

namespace StudyPlatform.ViewComponent
{
    public class SubjectsViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly IUserService _userService;

        public SubjectsViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userModel = await _userService.GetHelloUserModel();

            if (userModel.ApprovedOrders != null && userModel.ApprovedOrders.Count > 0)
            {
                foreach (var order in userModel.ApprovedOrders)
                {
                    foreach (var subject in order.Subjects)
                    {
                        if (subject.Subject.AbbrSubject == "math")
                        {
                            userModel.IsMathSubscription = true;
                            userModel.IsValidSubscription = true;
                        }
                        else if (subject.Subject.AbbrSubject == "history")
                        {
                            userModel.IsHistorySubscription = true;
                            userModel.IsValidSubscription = true;
                        }
                        else if (subject.Subject.AbbrSubject == "language")
                        {
                            userModel.IsUkranianSubscription = true;
                            userModel.IsValidSubscription = true;
                        }

                        // If all subscription types are set to true, mark IsAllSubscriptions as true
                        if (userModel.IsMathSubscription && userModel.IsHistorySubscription && userModel.IsUkranianSubscription)
                        {
                            userModel.IsAllSubscriptions = true;
                        }
                    }
                }
            }

            return View("Subjects" , userModel);
        }
    }
}
