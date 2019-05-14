using System.Linq;
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
}
