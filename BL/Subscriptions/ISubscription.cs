using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL.Subscriptions
{
    public interface ISubscription
    {
        Task<List<SubscriptionType>> GetAllSubTypes();
        Task<SubscriptionType> GetSubType(int subId);
    }
}
