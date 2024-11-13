using Microsoft.AspNetCore.Mvc;
using StudyPlatform.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using BL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using StudyPlatform.Middleware;
using StudyPlatform.ViewModels;

namespace StudyPlatform.Controllers
{
    //MiddleWare
    [SiteAuthorize()]
    public class BuyClassesController : Controller
    {
        private readonly List<string> validSubjects = new List<string>()
        {
            "math",
            "history",
            "language"
        };

        private readonly List<string> languageImages = new List<string>()
        {
            "~/Images/BuyClasses/Group 133.png",
            "~/Images/BuyClasses/book 1.png",
            "~/Images/BuyClasses/Group.png"
        };
        private readonly List<string> historyImages = new List<string>()
        {
            "~/Images/BuyClasses/hourglass 1.png",
            "~/Images/BuyClasses/Vector.png",
            "~/Images/BuyClasses/Layer_x0020_1.png"
        };
        private readonly List<string> mathImages = new List<string>()
        {
            "~/Images/BuyClasses/123 1.png",
            "~/Images/BuyClasses/tools 1.png",
            "~/Images/BuyClasses/cosine 1.png"
        };

        private readonly IUserService userService;

        public BuyClassesController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpGet]
        [Route("/myLessons")]
        public async Task<IActionResult> BuyClasses(int? klass,[Required] string subject)
        {

            if (!validSubjects.Contains(subject))
            {
                return Redirect("/");
            }

            var isSubscription = await userService.IsSubscription(subject);

            switch (subject)
            {
                case "math":
                    return View("BuyClasses", new BuyClassesViewModel()
                    {
                        Images = mathImages.Select(image => Url.Content(image)).ToList(),
                        ContentName = "МАТЕМАТИКА",
                        Abbr = subject,
                        IsSubscription = isSubscription,
                        Klass = klass
                    });
                case "language":
                    return View("BuyClasses", new BuyClassesViewModel()
                    {
                        Images = languageImages.Select(image => Url.Content(image)).ToList(),
                        Abbr = subject,
                        ContentName = "УКРАЇНСЬКА МОВА",
                        IsSubscription = isSubscription,
                        Klass = klass

                    });

                case "history":
                    return View("BuyClasses", new BuyClassesViewModel()
                    {
                        Images = historyImages.Select(image => Url.Content(image)).ToList(),
                        ContentName = "IСТОРIЯ",

                        Abbr = subject,
                        IsSubscription = isSubscription,
                        Klass = klass
                    });
            };


            return View("/");
        }
    }
}
