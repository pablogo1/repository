using System;
using System.Collections.Generic;
using Repository.Core;

namespace Repository.EF
{
    public abstract class Repository<TEntity, TId> : ReadOnlyRepository<TEntity, TId>, IRepository<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        public Repository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public void Add(TEntity entity)
        {
            DbContext.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbContext.AddRange(entities);
        }

        public void Delete(TEntity entity)
        {
            DbContext.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            DbContext.RemoveRange(entities);
        }
    }
}
