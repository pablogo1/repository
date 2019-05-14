using Repository.EF.Tests.Model;

namespace Repository.EF.Tests.Shared
{
    public class PostRepository : Repository<Post, int>, IPostRepository
    {
        public PostRepository(TestDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        }

        private new TestDbContext DbContext { get; }
    }
}
