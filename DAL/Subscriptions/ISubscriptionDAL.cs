using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Subscriptions
{
    public interface ISubscriptionDAL
    {
        Task<List<SubscriptionType>> GetAllSubscriptionTypes();
        Task<SubscriptionType?> Get(int id);
    }
}
