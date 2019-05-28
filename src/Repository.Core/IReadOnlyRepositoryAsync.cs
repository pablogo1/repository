using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Core
{
    public interface IReadOnlyRepositoryAsync<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        Task<TEntity> GetByIdAsync(TId id);
        Task<IEnumerable<TEntity>> AllAsync();
        Task<IEnumerable<TEntity>> AllAsync(int pageIndex, int pageSize);
        Task<IEnumerable<TEntity>> AllAsync(PagingOptions pagingOptions);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, PagingOptions pagingOptions);
    }
}
