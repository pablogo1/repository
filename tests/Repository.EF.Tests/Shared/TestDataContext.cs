using Repository.EF.Tests.Model;

namespace Repository.EF.Tests.Shared
{
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
