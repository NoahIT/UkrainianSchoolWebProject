using DAL.Models;

namespace StudyPlatform.ViewModels
{
    public class ScheduleViewModel
    {
        public List<Schedule> Schedules { get; set; } = new List<Schedule>();

        public List<Schedule> GetSchedule(string day, string time)
        {
            return Schedules.Where(s => s.Day == day && s.Time == time).ToList();
        }
    }
}
