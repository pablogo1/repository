using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Core
{
    public interface IReadOnlyRepository<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        TEntity GetById(TId id);
        Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

        IEnumerable<TEntity> All();
        Task<IEnumerable<TEntity>> AllAsync(CancellationToken cancellationToken = default);

        IEnumerable<TEntity> All(int pageIndex, int pageSize);
        Task<IEnumerable<TEntity>> AllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        IEnumerable<TEntity> All(PagingOptions pagingOptions);
        Task<IEnumerable<TEntity>> AllAsync(PagingOptions pagingOptions, CancellationToken cancellationToken = default);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, PagingOptions pagingOptions);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, PagingOptions pagingOptions, CancellationToken cancellationToken = default);
    }
}
