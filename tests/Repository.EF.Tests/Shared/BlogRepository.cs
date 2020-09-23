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
            return GetBlogWithAllPostsQuery
                .SingleOrDefault(o => o.BlogId == blogId);
        }

        public async Task<Blog> GetBlogWithAllPostsAsync(int blogId)
        {
            return await GetBlogWithAllPostsQuery
                .SingleOrDefaultAsync(o => o.BlogId == blogId).ConfigureAwait(false);
        }

        private IQueryable<Blog> GetBlogWithAllPostsQuery => DbContext
            .Blogs
            .Include(o => o.Posts);
    }
}
