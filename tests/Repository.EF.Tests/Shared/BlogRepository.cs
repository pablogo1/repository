using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.EF.Tests.Model;

namespace Repository.EF.Tests.Shared
{
    public class BlogRepository : Repository<Blog, int>, IBlogRepository
    {
        public BlogRepository(TestDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }

        private new TestDbContext DbContext { get; }

        public Blog GetBlogWithAllPosts(int blogId) 
        {
            return DbContext.Blogs
                .Include(o => o.Posts)
                .AsNoTracking()
                .SingleOrDefault(o => o.BlogId == blogId);
        }
    }

    public class BlogRepositoryAsync : RepositoryAsync<Blog, int>, IBlogRepositoryAsync
    {
        public BlogRepositoryAsync(TestDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }

        private new TestDbContext DbContext { get; }

        public Blog GetBlogWithAllPosts(int blogId)
        {
            return GetBlogWithAllPostsAsync(blogId).GetAwaiter().GetResult();
        }

        public async Task<Blog> GetBlogWithAllPostsAsync(int blogId)
        {
            return await DbContext.Blogs
                .Include(o => o.Posts)
                .AsNoTracking()
                .SingleOrDefaultAsync(o => o.BlogId == blogId);
        }
    }
}
