using BL.Helpers.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace BL.Helpers
{
    public class TransactionHelper : ITransactionHelper
    {
        private readonly PlatformDbContext context;

        public TransactionHelper(PlatformDbContext context)
        {
            this.context = context;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await context.Database.BeginTransactionAsync();
        }
    }
}
