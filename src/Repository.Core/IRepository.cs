using System.Collections.Generic;

namespace Repository.Core
{
    public interface IRepository<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
    }
}
