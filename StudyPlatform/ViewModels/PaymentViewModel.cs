using DAL.Models;

namespace StudyPlatform.ViewModels
{
    public class PaymentViewModel
    {
        public Order Order { get; set; }
        public string Signature { get; set; }
        public string OrderDate { get; set; }
        public string Productname { get; set; }
        public string OrderId { get; set; }
        public string Price { get; set; }
        public string? NextPay { get; set; }
        public string Start { get; set; }
        public string End { get; set; }

    }
}
