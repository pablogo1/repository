using Repository.EF.Tests.Model;

namespace Repository.EF.Tests.Shared
{
    public class TestDataContext : DataContext
    {
        public TestDataContext(TestDbContext dbContext)
            : base(dbContext)
        {
            DbContext = dbContext;
            BlogRepository = new BlogRepository(dbContext);
            PostRepository = new PostRepository(dbContext);
        }

        public IBlogRepository BlogRepository { get; }
        public IPostRepository PostRepository { get; }

        public new TestDbContext DbContext { get; }
    }
}
