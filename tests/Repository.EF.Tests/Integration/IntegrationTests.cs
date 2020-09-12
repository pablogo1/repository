using Repository.EF.Tests.Shared;
using Xunit;
using System.Threading.Tasks;

namespace Repository.EF.Tests.Integration
{
    public class IntegrationTests : IClassFixture<TestDatabaseFixture>
    {
        private readonly TestDataContext dataContext;
        private readonly IBlogRepository blogRepository;
        
        public IntegrationTests(TestDatabaseFixture fixture)
        {
            dataContext = fixture.DataContextFactory.CreateTestDataContext();
            blogRepository = dataContext.BlogRepository;
        }

        [Fact]
        public async Task UpdateAnExistingRecordAsync()
        {
            const int blogId = 1;
            const string updatedBlogUrl = "http://updated.url/blog";

            var blog = await blogRepository.GetByIdAsync(blogId);

            Assert.NotNull(blog);

            blog.Url = updatedBlogUrl;

            await dataContext.CommitAsync();

            blog = await blogRepository.GetByIdAsync(blogId);

            Assert.Equal(updatedBlogUrl, blog.Url);

            Assert.Empty(blog.Posts);

            blog = await blogRepository.GetBlogWithAllPostsAsync(blogId);
            Assert.NotEmpty(blog.Posts);

            blog = await blogRepository.GetByIdAsync(3);
            blog.Posts.Add(new Model.Post {
                Blog = blog,
                PostId = 15,
                Title = "This is a new post",
                Content = "This is a test content"
            });

            await dataContext.CommitAsync();

            blog = await blogRepository.GetBlogWithAllPostsAsync(3);
            Assert.NotNull(blog);
            Assert.NotEmpty(blog.Posts);

            blog.Posts.Add(new Model.Post {
                Blog = blog,
                PostId = 16,
                Title = "This is a new post title",
                Content = "This is a test content this is a content"
            });

            await dataContext.CommitAsync();

            blog = await blogRepository.GetBlogWithAllPostsAsync(3);
            
            Assert.Equal(2, blog.Posts.Count);
        }

        [Fact]
        public void UpdateAnExistingRecord()
        {
            const int blogId = 1;
            const string updatedBlogUrl = "http://updated.url/blog";

            var blog = blogRepository.GetById(blogId);

            Assert.NotNull(blog);

            blog.Url = updatedBlogUrl;

            dataContext.Commit();

            blog = blogRepository.GetById(blogId);

            Assert.Equal(updatedBlogUrl, blog.Url);
            Assert.Empty(blog.Posts);

            blog = blogRepository.GetBlogWithAllPosts(blogId);
            Assert.NotEmpty(blog.Posts);

            blog = blogRepository.GetById(3);
            blog.Posts.Add(new Model.Post {
                Blog = blog,
                PostId = 25,
                Title = "This is a new post",
                Content = "this is a test content"
            });

            dataContext.Commit();

            blog = blogRepository.GetBlogWithAllPosts(3);
            Assert.NotNull(blog);
            Assert.NotEmpty(blog.Posts);

            blog.Posts.Add(new Model.Post {
                Blog = blog,
                PostId = 26,
                Title = "This is a new post title",
                Content = "This is a test content"
            });

            dataContext.Commit();

            blog = blogRepository.GetBlogWithAllPosts(3);

            Assert.Equal(2, blog.Posts.Count);
        }
    }
}