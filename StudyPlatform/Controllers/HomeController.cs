using Microsoft.AspNetCore.Mvc;
using StudyPlatform.Models;
using System.Diagnostics;
using BL.Email;
using BL.Models;
using BL.ScheduleF;
using BL.Services;
using DAL.Models;
using Newtonsoft.Json;
using StudyPlatform.ViewModels;

namespace StudyPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly INotificationService notificationService;
        private readonly IScheduleBL schedule;

        public HomeController(IUserService userService, INotificationService notificationService, IScheduleBL schedule)
        {
            this.userService = userService;
            this.notificationService = notificationService;
            this.schedule = schedule;
        }

        [HttpGet]
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            var accessModel = await userService.GetAccessModel();

            if (accessModel.IsStudent)
            {
                var userModel = await userService.GetHelloUserLoginAndSubsValid();

                List<Lessons>? lessons = null;

                if (accessModel.IsSubscription)
                {
                    lessons = await userService.GetLessonsByValidSubscription();
                }

                var id = await userService.GetUserId();

                if (id != null)
                {
                    var mySchedules = await schedule.GetScheduleByUserId((int)id);

                    return View("HelloUser", new HelloUserViewModel()
                    {
                        IsValidSubscriptions = userModel.IsValidSubscription ?? false,
                        Login = userModel.Login ?? "",
                        Lessons = lessons,
                        MySchedule = mySchedules ?? new List<Schedule>()
                    });
                }

            }

            if (accessModel.IsAdmin)
            {
                
            }

            if (accessModel.IsTeacher)
            {
                
            }

            return View("Index");
        }

        [HttpGet]
        [Route("/calendar")]
        public IActionResult Calendar()
        {
            return View("Calendar");
        }

        [HttpGet]
        [Route("/howThisWorks")]
        public IActionResult HowThisWorks()
        {
            return View("HowThisWorks");
        }

        [HttpPost]
        [Route("/profile/update-information")]
        public async Task<IActionResult> UpdateProfileInfo(
                                                            string? fullName,
                                                            string? login,
                                                            string? password,
                                                            string? phone,
                                                            int userId,
                                                            string email
                                                            )
        {
            await userService.UpdateProfileInformation(fullName, login, password, phone, userId);

            await notificationService.SendEmailNoReply(new MailMessageCustom(
                "Изменения данных вашего аккаунта.",
                "Уважаемый пользователь, данные вашего аккаунта были изменены, если это были не вы, востановите пароль и замените его",
                email));

            return Redirect("/");
        }
    }

}
