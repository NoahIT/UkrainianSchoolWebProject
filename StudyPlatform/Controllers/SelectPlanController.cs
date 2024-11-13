using Microsoft.AspNetCore.Mvc;
using StudyPlatform.Models;
using System.Diagnostics;
using BL.Subscriptions;
using DAL.Models;
using StudyPlatform.ViewModels;
using BL.ScheduleF;

namespace StudyPlatform.Controllers
{
    public class SelectPlanController : Controller
    {
        private readonly ISubscription sub;
        private readonly IScheduleBL schedule;

        public SelectPlanController(ISubscription sub, IScheduleBL schedule)
        {
            this.sub = sub;
            this.schedule = schedule;
        }

        [HttpGet]
        [Route("/selectPlan")]
        public async Task<IActionResult> SelectPlan()
        {
            return View("SelectPlan", new SelectPlanViewModel
            {
                SubscriptionTypes = await sub.GetAllSubTypes(),
            });
        }
    }
}
