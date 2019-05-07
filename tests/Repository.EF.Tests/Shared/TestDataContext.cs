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
    public class TestDataContext : DataContext
    {
        public TestDataContext(TestDbContext dbContext) : this(dbContext, null, null)
        {

        }

        public TestDataContext(TestDbContext dbContext, IBlogRepository blogRepository, IPostRepository postRepository)
            : base(dbContext)
        {
            BlogRepository = blogRepository;
            PostRepository = postRepository;
        }

        public IBlogRepository BlogRepository { get; }
        public IPostRepository PostRepository { get; }
    }
}
