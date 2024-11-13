using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SubscriptionType
    {
        public int Id { get; set; }
        public int Months { get; set; }
        public decimal Price { get; set; }
        public int LessonsCount { get; set; } 
        public string Discount { get; set; }
        public decimal FirstPrice { get; set; }
    }
}
