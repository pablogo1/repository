using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repository.Core;

namespace Repository.EF
{
    public abstract class ReadOnlyRepository<TEntity, TId> : IReadOnlyRepository<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        protected ReadOnlyRepository(DbContext dbContext)
        {
            this.DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<TEntity> All()
        {
            return All(null);
        }

        public IEnumerable<TEntity> All(int pageIndex, int pageSize)
        {
            var pagingOptions = new PagingOptions 
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            return All(pagingOptions);
        }

        public IEnumerable<TEntity> All(PagingOptions pagingOptions)
        {
            return DbContext.Set<TEntity>()
                .Page(pagingOptions)
                .ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Find(predicate, null);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize)
        {
            var pagingOptions = new PagingOptions
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            return Find(predicate, pagingOptions);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, PagingOptions pagingOptions)
        {
            return DbContext.Set<TEntity>()
                .Where(predicate)
                .Page(pagingOptions)
                .ToList();
        }

        public TEntity GetById(TId id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        protected DbContext DbContext { get; }
    }
}
