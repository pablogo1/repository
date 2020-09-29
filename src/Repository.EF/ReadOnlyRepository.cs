using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<TEntity>> AllAsync(CancellationToken cancellationToken = default)
        {
            return await AllAsync(null, cancellationToken);
        }

        public IEnumerable<TEntity> All(int pageIndex, int pageSize)
        {
            var pagingOptions = PagingOptions.Create(pageIndex, pageSize);

            return All(pagingOptions);
        }

        public async Task<IEnumerable<TEntity>> AllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var pagingOptions = PagingOptions.Create(pageIndex, pageSize);

            return await AllAsync(pagingOptions, cancellationToken);
        }

        public IEnumerable<TEntity> All(PagingOptions pagingOptions)
        {
            return DbContext.Set<TEntity>()
                .Page(pagingOptions)
                .ToList();
        }

        public async Task<IEnumerable<TEntity>> AllAsync(PagingOptions pagingOptions, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TEntity>()
                .Page(pagingOptions)
                .ToListAsync(cancellationToken);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Find(predicate, null);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await FindAsync(predicate, null, cancellationToken);
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

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var pagingOptions = PagingOptions.Create(pageIndex, pageSize);

            return await FindAsync(predicate, pagingOptions, cancellationToken);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, PagingOptions pagingOptions)
        {
            return DbContext.Set<TEntity>()
                .Where(predicate)
                .Page(pagingOptions)
                .ToList();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, PagingOptions pagingOptions, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TEntity>()
                .Where(predicate)
                .Page(pagingOptions)
                .ToListAsync(cancellationToken);
        }

        public TEntity GetById(TId id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
        }

        protected DbContext DbContext { get; }
    }
}
