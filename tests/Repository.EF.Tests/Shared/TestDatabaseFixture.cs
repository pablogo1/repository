using Repository.EF.Tests.Model;
using System.Linq;

namespace Repository.EF.Tests.Shared
{
    public class TestDatabaseFixture
    {
        public TestDatabaseFixture()
        {
            var dbContextFactory = new InMemoryDbContextFactory();
            DbContext = dbContextFactory.CreateDbContext("test");

            IBlogRepository blogRepository = new BlogRepository(DbContext);
            IPostRepository postRepository = new PostRepository(DbContext);
            
            DataContext = new TestDataContext(DbContext, blogRepository, postRepository);
        }

        public TestDataContext DataContext { get; }
        public TestDbContext DbContext { get; }
    }
}
