using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Subscriptions
{
    public class SubscriptionDAL : ISubscriptionDAL
    {
        private readonly PlatformDbContext context;

        public SubscriptionDAL(PlatformDbContext context)
        {
            this.context = context;
        }

        public async Task<List<SubscriptionType>> GetAllSubscriptionTypes()
        {
            return await context.SubscriptionTypes.ToListAsync();
        }

        public async Task<SubscriptionType?> Get(int id)
        {
            return await context.SubscriptionTypes.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
