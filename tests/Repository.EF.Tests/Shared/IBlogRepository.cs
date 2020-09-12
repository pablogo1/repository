using System.Threading.Tasks;
using Repository.Core;
using Repository.EF.Tests.Model;

namespace Repository.EF.Tests.Shared
{
    public interface IBlogRepository : IRepository<Blog, int>
    {
        Blog GetBlogWithAllPosts(int blogId);
        Task<Blog> GetBlogWithAllPostsAsync(int blogId);
    }
}
