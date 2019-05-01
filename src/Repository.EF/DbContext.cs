using System;
using System.Threading.Tasks;
using Repository.Core;

namespace Repository.EF
{
    public abstract class DbContext : IDbContext
    {
        private readonly Microsoft.EntityFrameworkCore.DbContext dbContext;

        public DbContext(Microsoft.EntityFrameworkCore.DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}