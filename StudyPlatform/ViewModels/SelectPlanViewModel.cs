using DAL.Models;

namespace StudyPlatform.ViewModels
{
    public class SelectPlanViewModel
    {
        public List<SubscriptionType> SubscriptionTypes { get; set; }

        public ScheduleViewModel ScheduleViewModel { get; set; }
    }
}
