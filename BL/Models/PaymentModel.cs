using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL.Models
{
    public class PaymentModel
    {
        public Order Order { get; set; }
        public string Signature { get; set; }
        public string OrderDate { get; set; }
        public string Productname { get; set; }
        public string OrderId { get; set; }
        public string Price { get; set; }
        public string NextPay { get; set; }
        public string Start { get; set; }
        public string End { get; set; }

        public bool EqualsNextPayEnd()
        {
            if (this.NextPay.Length != this.End.Length)
            {
                return false;
            }

            for (int i = 0; i < this.End.Length; i++)
            {
                if (this.End[i] != this.NextPay[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
