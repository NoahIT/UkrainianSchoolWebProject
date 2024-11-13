using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Subscriptions.Payments
{
    public interface IPaymentDAL
    {
        Task SavePaymentToken(int userId, string token);

        Task SavePaymentHistory(PaymentsHistory history);
    }
}
