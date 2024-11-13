using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Subscriptions;

namespace BL.Subscriptions
{
    public class Subscription : ISubscription
    {
        private readonly ISubscriptionDAL sub;

        public Subscription(ISubscriptionDAL sub)
        {
            this.sub = sub;
        }

        public async Task<List<SubscriptionType>> GetAllSubTypes()
        {
            return await sub.GetAllSubscriptionTypes();
        }

        public async Task<SubscriptionType> GetSubType(int subId)
        {
            return await sub.Get(subId);
        }
    }
}
