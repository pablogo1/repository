using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Core;

namespace Repository.EF
{
    public abstract class RepositoryAsync<TEntity, TId> : ReadOnlyRepositoryAsync<TEntity, TId>, IRepositoryAsync<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        protected RepositoryAsync(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddAsync(TEntity entity)
        {
            await DbContext.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await DbContext.AddRangeAsync(entities);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => DbContext.Remove(entity));
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => DbContext.RemoveRange(entities));
        }
    }
}