using DAL.Models;

namespace StudyPlatform.ViewModels
{
    public class HelloUserViewModel
    {
         public bool IsValidSubscriptions { get; set; }
         public string Login { get; set; } = string.Empty;

         public List<Lessons>? Lessons { get; set; }

         public List<Schedule> MySchedule { get; set; }
    }
}
