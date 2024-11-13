using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PaymentToken : Entity
    {
        public virtual User User { get; set; }
        public int UserId { get; set; }

        public string RecToken { get; set; }
    }
}
