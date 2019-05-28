using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Core;

namespace Repository.EF
{
    public abstract class ReadOnlyRepositoryAsync<TEntity, TId> : IReadOnlyRepositoryAsync<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        protected ReadOnlyRepositoryAsync(DbContext dbContext)
        {
            this.DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await AllAsync(null);
        }

        public async Task<IEnumerable<TEntity>> AllAsync(int pageIndex, int pageSize)
        {
            var pagingOptions = PagingOptions.Create(pageIndex, pageSize);

            return await AllAsync(pagingOptions);
        }

        public async Task<IEnumerable<TEntity>> AllAsync(PagingOptions pagingOptions)
        {
            return await DbContext.Set<TEntity>()
                .Page(pagingOptions)
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await FindAsync(predicate, null);        
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize)
        {
            var pagingOptions = PagingOptions.Create(pageIndex, pageSize);

            return await FindAsync(predicate, pagingOptions);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, PagingOptions pagingOptions)
        {
            return await DbContext.Set<TEntity>()
                .Page(pagingOptions)
                .ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }

        protected DbContext DbContext { get; }
    }
}