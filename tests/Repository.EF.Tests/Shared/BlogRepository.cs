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

        protected override IQueryable<Blog> ObjectSet => DbContext.Blogs;

        public override Blog GetById(int id)
        {
            return ObjectSet.SingleOrDefault(o => o.BlogId == id);
        }

        public Blog GetBlogWithAllPosts(int blogId) 
        {
            return DbContext.Blogs
                .Include(o => o.Posts)
                .AsNoTracking()
                .SingleOrDefault(o => o.BlogId == blogId);
        }
    }
}
