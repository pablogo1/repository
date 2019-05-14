using Microsoft.EntityFrameworkCore;
using Repository.EF.Tests.Model;

namespace Repository.EF.Tests.Shared
{
    public class InMemoryDbContextFactory
    {
        public TestDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "test")
                .Options;
            var dbContext = new TestDbContext(options);

            return dbContext;
        }
    }
}
