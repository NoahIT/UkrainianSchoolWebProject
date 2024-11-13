using Microsoft.AspNetCore.Mvc;
using StudyPlatform.Models;
using StudyPlatform.ViewModels;
using System.Diagnostics;
using BL.Email;
using DAL.Models;

namespace StudyPlatform.Controllers
{
    public class QuestionareController : Controller
    {
        private readonly INotificationService sender;

        public QuestionareController(INotificationService sender)
        {
            this.sender = sender;
        }

        [HttpGet]
        [Route("/entryMessage")]
        public IActionResult EntryMessage() // входной, без параметра
        {
            return View("EntryMessage");
        }

        [HttpPost]
        [Route("/knowlegeRating")] // оценка звездочками, 1,2,3,4,5 stars
        public IActionResult KnowlegeRating()
        {
            return View("KnowlegeRating");
        }

        [HttpPost]
        [Route("/ourTeachers")] // учителя, без параметра
        public IActionResult OurTeachers(QuestionareViewModel model)
        {
            return View("OurTeachers", model);
        }

        [HttpPost]
        [Route("/motivation")]  // оценка звездочками, 1,2,3,4,5 stars
        public IActionResult Motivation(QuestionareViewModel model) 
        {
            return View("Motivation", model);
        }

        [HttpPost]
        [Route("/learningFormat")] // формат обучения, 4 варианта checkbox /////TUT
        public IActionResult LearningFormat(QuestionareViewModel model)
        {
            return View("LearningFormat", model);
        }
        [HttpPost]
        [Route("/selectClass")] // цель, 2 checkbox
        public IActionResult SelectClass(QuestionareViewModel model)
        {
            return View("SelectClass", model);
        }

        [HttpPost]
        [Route("/learningTarget")] //  3 checkbox
        public IActionResult LearningTarget(QuestionareViewModel model)
        {
            return View("LearningTarget", model);
        }


        [HttpPost]
        [Route("/studyModules")]
        public IActionResult StudyModules(QuestionareViewModel model) // цель, 3 checkbox
        {
            return View("StudyModules", model);
        }

        [HttpPost]
        [Route("/ourSchoolPerks")]
        public IActionResult OurSchoolPerks(QuestionareViewModel model)
        {
            return View("OurSchoolPerks", model);
        }

        [HttpPost]
        [Route("/hoursPerWeek")] // 3 checkbox
        public IActionResult HoursPerWeek(QuestionareViewModel model)
        {
            return View("HoursPerWeek", model);
        }


        [HttpPost]
        [Route("/enterGmailCredentials")] // gmail, finish
        public IActionResult EnterGmailCredentials(QuestionareViewModel model)
        {
            return View("EnterGmailCredentials", model);
        }

        [HttpPost]
        [Route("/sendToAnalytics")] // gmail, finish
        public async Task<IActionResult> SendAnalyticts(QuestionareViewModel model)
        {
            await sender.SendEmailNoReply(new MailMessageCustom("Statistika Kirilui", model.ToString(), "vovancase@gmail.com"));

            return Redirect("/reviewFeatures"); 
        }

        [HttpGet]
        [Route("/reviewFeatures")]
        public async Task<IActionResult> ReviewFeatures()
        {
            return View("ReviewPlafromFeatures");
        }
    }
}
