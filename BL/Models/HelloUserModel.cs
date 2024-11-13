using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL.Models
{
    public class HelloUserModel
    {
        public string? Login { get; set; }
        public List<Order>? ApprovedOrders { get; set; }
        public bool IsValidSubscription { get; set; } = false;
        public bool IsAllSubscriptions { get; set; } = false;
        public bool IsMathSubscription { get; set; } = false;
        public bool IsHistorySubscription { get; set; } = false;
        public bool IsUkranianSubscription { get; set; } = false;

    }

}
