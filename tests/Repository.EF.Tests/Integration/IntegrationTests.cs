using Repository.EF.Tests.Shared;
using Xunit;
using System.Threading.Tasks;
using Repository.EF.Tests.Model;

namespace Repository.EF.Tests.Integration
{
    public class IntegrationTests : IClassFixture<TestDatabaseFixture>
    {
        private readonly TestDataContext dataContext;
        private readonly IBlogRepository blogRepository;
        
        public IntegrationTests(TestDatabaseFixture fixture)
        {
            if (fixture is null)
            {
                throw new System.ArgumentNullException(nameof(fixture));
            }

            dataContext = fixture.DataContextFactory.CreateTestDataContext();
            blogRepository = dataContext.BlogRepository;
        }

        [Fact]
        public async Task UpdateAnExistingRecordAsync()
        {
            const int blogId = 1;
            const string updatedBlogUrl = "http://updated.url/blog2";

            var blog = await blogRepository.GetByIdAsync(blogId).ConfigureAwait(false);

            Assert.NotNull(blog);

            blog.Url = updatedBlogUrl;

            await dataContext.CommitAsync().ConfigureAwait(false);

            blog = await blogRepository.GetByIdAsync(blogId).ConfigureAwait(false);

            Assert.Equal(updatedBlogUrl, blog.Url);
        }

        [Fact]
        public async Task AddANewRecordAsync()
        {
            const int newBlogId = 998;
            Blog newBlog = new Blog
            {
                BlogId = newBlogId,
                Url = "a.new.url"
            };

            await blogRepository.AddAsync(newBlog).ConfigureAwait(false);

            await dataContext.CommitAsync().ConfigureAwait(false);

            Blog blog = await blogRepository.GetByIdAsync(newBlogId).ConfigureAwait(false);
            Assert.NotNull(blog);
        }

        [Fact]
        public async Task DeleteARecordAsync()
        {
            const int blogIdToDelete = 881;
            Blog newBlog = new Blog
            {
                BlogId = blogIdToDelete,
                Url = "test url"
            };

            await blogRepository.AddAsync(newBlog).ConfigureAwait(false);
            await dataContext.CommitAsync().ConfigureAwait(false);

            Blog foundBlog = await blogRepository.GetByIdAsync(blogIdToDelete).ConfigureAwait(false);
            Assert.NotNull(foundBlog);

            blogRepository.Delete(foundBlog);
            Assert.NotNull(foundBlog);

            await dataContext.CommitAsync().ConfigureAwait(false);

            foundBlog = await blogRepository.GetByIdAsync(blogIdToDelete).ConfigureAwait(false);
            Assert.Null(foundBlog);
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
        }

        [Fact]
        public void AddANewRecord()
        {
            const int newBlogId = 997;
            Blog newBlog = new Blog
            {
                BlogId = newBlogId,
                Url = "a.new.url"
            };

            blogRepository.Add(newBlog);

            dataContext.Commit();

            Blog blog = blogRepository.GetById(newBlogId);
            Assert.NotNull(blog);
        }

        [Fact]
        public void DeleteARecord()
        {
            const int blogIdToDelete = 888;
            Blog newBlog = new Blog
            {
                BlogId = blogIdToDelete,
                Url = "test url"
            };

            blogRepository.Add(newBlog);

            dataContext.Commit();

            Blog foundBlog = blogRepository.GetById(blogIdToDelete);
            Assert.NotNull(foundBlog);

            blogRepository.Delete(foundBlog);
            Assert.NotNull(foundBlog);

            dataContext.Commit();

            foundBlog = blogRepository.GetById(blogIdToDelete);
            Assert.Null(foundBlog);
        }

        [Fact]
        public void TestName()
        {
        //Given
        
        //When
        
        //Then
        }   
    }
}
