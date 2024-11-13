using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Coupon : Entity
    {
        public string Code { get; set; }
        public decimal DiscountPercentage { get; set; } // Размер скидки
        public bool IsUsed { get; set; } // Флаг, указывающий, использован ли купон
        public bool IsActive { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
