using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repository.Core
{
    public interface IReadOnlyRepository<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        TEntity GetById(TId id);
        IEnumerable<TEntity> All();
        IEnumerable<TEntity> All(int pageIndex, int pageSize);
        IEnumerable<TEntity> All(PagingOptions pagingOptions);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, PagingOptions pagingOptions);
    }
}
