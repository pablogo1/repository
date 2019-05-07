using System.Linq;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using Repository.EF.Tests.Model;

namespace Repository.EF.Tests.Shared
{
    public interface IPostRepository : IRepository<Post, int>
    {
    }

    public class PostRepository : Repository<Post, int>, IPostRepository
    {
        public PostRepository(TestDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        }

        private new TestDbContext DbContext { get; }

        protected override IQueryable<Post> ObjectSet => DbContext.Posts;

        public override Post GetById(int id) => ObjectSet.SingleOrDefault(o => o.PostId == id);
    }
}
