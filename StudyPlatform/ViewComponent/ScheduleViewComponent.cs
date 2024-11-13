using BL.ScheduleF;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using StudyPlatform.ViewModels;

namespace StudyPlatform.ViewComponent
{
    public class ScheduleViewComponent :  Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly IScheduleBL schedule;

        public ScheduleViewComponent(IScheduleBL schedule)
        {
            this.schedule = schedule;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<Schedule>? schedules = null)
        {
            List<Schedule> model;
            if (schedules == null)
            {
                model = await schedule.GetAllSchedules();
            }
            else
            {
                model = schedules;
            }

            return View("Schedule", new ScheduleViewModel()
            {
                Schedules = model
            });
        }

    }
}
