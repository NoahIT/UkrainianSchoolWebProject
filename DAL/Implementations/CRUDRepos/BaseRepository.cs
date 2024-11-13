using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations.CRUDRepos
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {

        protected PlatformDbContext Context;
        protected DbSet<TEntity> Entities;

        public BaseRepository(PlatformDbContext context)
        {
            Context = context;
            Entities = Context.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> ListAsync(params Expression<Func<TEntity, object>>[] properties)
        {
            IQueryable<TEntity> query = Entities;

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();

        }

        public async Task<TEntity?> GetAsync(int id)
        {
            return await Entities
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            Context.Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity != null)
            {
                Context.Remove(entity);
                await Context.SaveChangesAsync();
            }
        }
    }
}
