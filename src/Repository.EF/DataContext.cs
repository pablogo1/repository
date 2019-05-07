using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Core;

namespace Repository.EF
{
    public abstract class DataContext : IDataContext
    {
        protected DataContext(DbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        protected DbContext DbContext { get; }
    }
}