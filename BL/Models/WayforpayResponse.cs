using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class WayforpayResponse
    {
        public string MerchantAccount { get; set; }
        public string OrderReference { get; set; }
        public string MerchantSignature { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string AuthCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long CreatedDate { get; set; } // Unix timestamp
        public long ProcessingDate { get; set; } // Unix timestamp
        public string CardPan { get; set; }
        public string CardType { get; set; }
        public string IssuerBankCountry { get; set; }
        public string IssuerBankName { get; set; }
        public string RecToken { get; set; }
        public string TransactionStatus { get; set; }
        public string Reason { get; set; }
        public int ReasonCode { get; set; }
        public decimal Fee { get; set; }
        public string PaymentSystem { get; set; }
        public string AcquirerBankName { get; set; }
        public string CardProduct { get; set; }
        public string ClientName { get; set; }

    }
}
