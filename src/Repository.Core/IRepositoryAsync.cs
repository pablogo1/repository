using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Core
{
    public interface IRepositoryAsync<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    }
}
