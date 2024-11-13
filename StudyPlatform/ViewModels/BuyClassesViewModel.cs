using DAL.Models;

namespace StudyPlatform.ViewModels
{
    public class BuyClassesViewModel
    {
        public List<string> Images { get; set; } = new List<string>();
        public string ContentName { get; set; } = string.Empty;
        public string Abbr { get; set; } = string.Empty;
        public bool IsSubscription { get; set; }
        public int? Klass { get; set; }
    }
}
