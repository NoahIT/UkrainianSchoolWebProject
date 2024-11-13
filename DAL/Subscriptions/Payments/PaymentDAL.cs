using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Subscriptions.Payments
{
    public class PaymentDAL : IPaymentDAL
    {
        private readonly PlatformDbContext context;

        public PaymentDAL(PlatformDbContext context)
        {
            this.context = context;
        }

        public async Task SavePaymentToken(int userId, string token)
        {
            context.PaymentTokens.Add(new PaymentToken()
            {
                UserId = userId,
                RecToken = token
            });

            await context.SaveChangesAsync();
        }

        public async Task SavePaymentHistory(PaymentsHistory history)
        {
            await context.PaymentsHistories.AddAsync(history);

            await context.SaveChangesAsync();
        }
    }
}
