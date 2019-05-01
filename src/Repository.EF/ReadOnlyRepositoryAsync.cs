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
        private readonly Microsoft.EntityFrameworkCore.DbContext dbContext;

        public ReadOnlyRepositoryAsync(Microsoft.EntityFrameworkCore.DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<IEnumerable<TEntity>> AllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> AllAsync(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<bool, TEntity>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<bool, TEntity>> predicate, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public abstract Task<TEntity> GetByIdAsync(TId id);

        protected abstract IQueryable<TEntity> ObjectSet { get; }

        protected Microsoft.EntityFrameworkCore.DbContext DbContext => dbContext;
    }
}