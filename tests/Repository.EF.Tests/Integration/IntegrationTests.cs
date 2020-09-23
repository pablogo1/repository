using Repository.EF.Tests.Shared;
using Xunit;
using System;
using System.Threading.Tasks;
using System.Linq;

using Repository.EF.Tests.Model;
using System.Linq.Expressions;

namespace Repository.EF.Tests.Integration
{
    public class IntegrationTests : IClassFixture<TestDatabaseFixture>
    {
        private readonly TestDataContext dataContext;
        private readonly IBlogRepository blogRepository;
        private readonly IPostRepository postRepository;
        
        public IntegrationTests(TestDatabaseFixture fixture)
        {
            if (fixture is null)
            {
                throw new System.ArgumentNullException(nameof(fixture));
            }

            dataContext = fixture.DataContextFactory.CreateTestDataContext();
            blogRepository = dataContext.BlogRepository;
            postRepository = dataContext.PostRepository;
        }

        [Fact]
        public async Task CreateANewBlogAsync()
        {
            const int newBlogId = 888;
            var newBlog = CreateBlog(newBlogId);

            await blogRepository.AddAsync(newBlog);
            await dataContext.CommitAsync();

            var savedBlog = await dataContext.DbContext.Blogs.FindAsync(newBlogId).ConfigureAwait(false);
            Assert.NotNull(newBlog);
            Assert.Equal(newBlog, savedBlog);
            Assert.NotEmpty(newBlog.Posts);

            await DeleteBlogFromDbContextAsync(savedBlog);
        }

        [Fact]
        public void CreateANewBlog()
        {
            const int newBlogId = 888;
            Blog newBlog = CreateBlog(newBlogId);

            blogRepository.Add(newBlog);
            dataContext.Commit();

            var savedBlog = dataContext.DbContext.Blogs.Find(newBlogId);
            Assert.NotNull(newBlog);
            Assert.Equal(newBlog, savedBlog);
            Assert.NotEmpty(newBlog.Posts);

            DeleteBlogFromDbContext(savedBlog);
        }

        [Fact]
        public async Task RetrieveExistingBlogByIdAsync()
        {
            const int blogId = 1;
            var expectedBlog = await dataContext.DbContext.Blogs.FindAsync(blogId);

            var actualBlog = await blogRepository.GetByIdAsync(blogId);

            Assert.NotNull(actualBlog);
            Assert.Equal(expectedBlog, actualBlog);
        }

        [Fact]
        public void RetrieveExistingBlogById()
        {
            const int blogId = 1;
            var expectedBlog = dataContext.DbContext.Blogs.Find(blogId);

            var actualBlog = blogRepository.GetById(blogId);

            Assert.NotNull(actualBlog);
            Assert.Equal(expectedBlog, actualBlog);
        }

        [Fact]
        public async Task RetrieveAllExistingBlogsAsync()
        {
            var expectedBlogs = dataContext.DbContext.Blogs.ToList();

            var actualBlogs = await blogRepository.AllAsync();

            Assert.NotNull(actualBlogs);
            Assert.Equal(expectedBlogs, actualBlogs);
        }

        [Fact]
        public void RetrieveAllExistingBlogs()
        {
            var expectedBlogs = dataContext.DbContext.Blogs.ToList();

            var actualBlogs = blogRepository.All();

            Assert.NotNull(actualBlogs);
            Assert.Equal(expectedBlogs, actualBlogs);
        }

        [Fact]
        public async Task RetrieveAllExistingBlogsWithPagingOptionsAsync()
        {
            const int pageSize = 5;
            const int pageIndex = 2;

            var expectedBlogs = dataContext.DbContext.Blogs.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();

            var actualBlogs = await blogRepository.AllAsync(pageIndex, pageSize);

            Assert.NotNull(actualBlogs);
            Assert.Equal(expectedBlogs, actualBlogs);
        }

        [Fact]
        public void RetrieveAllExistingBlogsWithPagingOptions()
        {
            const int pageSize = 5;
            const int pageIndex = 2;

            var expectedBlogs = dataContext.DbContext.Blogs.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();

            var actualBlogs = blogRepository.All(pageIndex, pageSize);
            
            Assert.NotNull(actualBlogs);
            Assert.NotEqual(expectedBlogs, actualBlogs);
        }

        [Fact]
        public async Task RetrieveMatchingBlogsAsync()
        {
            Expression<Func<Blog, bool>> predicate = blog => blog.BlogId % 2 == 0;
            var expectedBlogs = dataContext.DbContext.Blogs.Where(predicate).ToList();

            var actualBlogs = await blogRepository.FindAsync(predicate).ConfigureAwait(false);

            Assert.NotNull(actualBlogs);
            Assert.Equal(expectedBlogs, actualBlogs);
        }

        [Fact]
        public void RetrieveMatchingBlogs()
        {
            Expression<Func<Blog, bool>> predicate = blog => blog.BlogId % 2 == 0;
            var expectedBlogs = dataContext.DbContext.Blogs.Where(predicate).ToList();

            var actualBlog = blogRepository.Find(predicate);

            Assert.NotNull(actualBlog);
            Assert.Equal(expectedBlogs, actualBlog);
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

        private static Blog CreateBlog(int newBlogId, bool addPost = true)
        {
            var newBlog = new Blog
            {
                BlogId = newBlogId,
                Url = "a new url"
            };
            newBlog.Posts.Add(new Post()
            {
                BlogId = newBlogId,
                PostId = 881,
                Title = "A new post",
                Content = "A new content for my new blog"
            });

            return newBlog;
        }

        private async Task DeleteBlogFromDbContextAsync(Blog blog)
        {
            dataContext.DbContext.Blogs.Remove(blog);
            await dataContext.DbContext.SaveChangesAsync();
        }

        private void DeleteBlogFromDbContext(Blog blog) => DeleteBlogFromDbContextAsync(blog).ConfigureAwait(false).GetAwaiter().GetResult();
    }
}
