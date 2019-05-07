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
            return ObjectSet.ToList();
        }

        public IEnumerable<TEntity> All(int pageIndex, int pageSize)
        {
            return ObjectSet
                .Skip(pageIndex)
                .Take(pageSize)
                .ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return ObjectSet.Where(predicate).ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize)
        {
            return ObjectSet.Where(predicate)
                .Skip(pageIndex)
                .Take(pageSize)
                .ToList();
        }

        public abstract TEntity GetById(TId id);

        protected abstract IQueryable<TEntity> ObjectSet { get; }

        protected DbContext DbContext { get; }
    }
}
