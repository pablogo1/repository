using Repository.EF.Tests.Model;
using Repository.EF.Tests.Shared;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Repository.EF.Tests.Integration
{
    public class ReadOnlyRepositoryIntegrationTests : IClassFixture<TestDatabaseFixture>
    {
        private readonly IBlogRepository repository;
        private readonly TestDbContext dbContext;

        public ReadOnlyRepositoryIntegrationTests(TestDatabaseFixture fixture)
        {
            if (fixture is null)
            {
                throw new ArgumentNullException(nameof(fixture));
            }

            var testDataContext = fixture.DataContextFactory.CreateTestDataContext();

            dbContext = testDataContext.DbContext;
            repository = testDataContext.BlogRepository;
        }

        [Fact]
        public void GetById_ReturnsCorrectItem()
        {
            //Arrange
            const int blogId = 2;
            var expectedBlog = dbContext.Blogs.Single(p => p.BlogId == blogId);

            //Act
            var actualBlog = repository.GetById(blogId);

            //Assert
            Assert.NotNull(actualBlog);
            Assert.Equal(blogId, actualBlog.BlogId);
            Assert.Equal(expectedBlog, actualBlog);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectItem()
        {
            const int blogId = 2;

            Blog actual = await repository.GetByIdAsync(blogId).ConfigureAwait(false);

            Assert.NotNull(actual);
            Assert.Equal(blogId, actual.BlogId);
        }

        [Fact]
        public void All_ReturnsAllItems()
        {
            //Arrange
            var expectedItemCount = dbContext.Blogs.Count();

            //Act
            var items = repository.All();

            //Assert
            Assert.NotNull(items);
            Assert.NotEmpty(items);
            Assert.Equal(expectedItemCount, items.ToList().Count);
        }

        [Fact]
        public async Task AllAsync_ReturnsAllItems()
        {
            // Arrange
            var expectedItems = dbContext.Blogs.ToList();

            // Act
            var items = await repository.AllAsync().ConfigureAwait(false);

            // Assert
            Assert.NotNull(items);
            Assert.NotEmpty(items);
            Assert.Equal(expectedItems, items);
        }

        [Theory]
        [InlineData(1, 5, 0, 4)]
        [InlineData(2, 5, 5, 9)]
        [InlineData(3, 2, 4, 5)]
        public void All_ReturnsCorrectElements_WhenPageIndexAndPageSizeAreProvided(int pageIndex, int pageSize, int startIndex, int endIndex)
        {
            // Arrange
            var blogs = dbContext.Blogs.ToList();
            var expectedBlogs = new List<Blog>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                expectedBlogs.Add(blogs[i]);
            }

            // Act
            var actualBlogs = repository.All(pageIndex, pageSize);

            // Assert
            Assert.NotNull(actualBlogs);
            Assert.NotEmpty(actualBlogs);
            Assert.Equal(expectedBlogs, actualBlogs);
        }

        [Theory]
        [InlineData(1, 5, 0, 4)]
        [InlineData(2, 5, 5, 9)]
        [InlineData(3, 2, 4, 5)]
        public async Task AllAsync_ReturnsCorrectElements_WhenPageIndexAndPageSizeAreProvided(int pageIndex, int pageSize, int startIndex, int endIndex)
        {
            // Arrange
            var blogs = dbContext.Blogs.ToList();
            var expectedBlogs = new List<Blog>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                expectedBlogs.Add(blogs[i]);
            }

            // Act
            var actualBlogs = await repository.AllAsync(pageIndex, pageSize).ConfigureAwait(false);

            // Assert
            Assert.NotNull(actualBlogs);
            Assert.NotEmpty(actualBlogs);
            Assert.Equal(expectedBlogs, actualBlogs);
        }

        [Fact]
        public void Find_ReturnsAllItemsThatMatchesPredicate()
        {
            //Arrange
            var expectedBlogs = dbContext.Blogs.Where(b => b.BlogId % 2 != 0)
                .ToList();
            var expectedItemCount = expectedBlogs.Count;

            //Act
            var actualBlogs = repository.Find(b => b.BlogId % 2 != 0);

            //Assert
            Assert.NotNull(actualBlogs);
            Assert.NotEmpty(actualBlogs);
            Assert.Equal(expectedBlogs, actualBlogs);
            Assert.Equal(expectedItemCount, actualBlogs.ToList().Count);
        }

        [Fact]
        public async Task FindAsync_ReturnsAllItemsThatMatchesPredicate()
        {
            //Arrange
            var expectedBlogs = dbContext.Blogs.Where(b => b.BlogId % 2 != 0)
                .ToList();
            var expectedItemCount = expectedBlogs.Count;

            //Act
            var actualBlogs = await repository.FindAsync(b => b.BlogId % 2 != 0).ConfigureAwait(false);

            //Assert
            Assert.NotNull(actualBlogs);
            Assert.NotEmpty(actualBlogs);
            Assert.Equal(expectedBlogs, actualBlogs);
            Assert.Equal(expectedItemCount, actualBlogs.ToList().Count);
        }

        [Theory]
        [InlineData(2, 3, 3, 4)]
        [InlineData(1, 2, 0, 1)]
        public void Find_ReturnsPagedItemsThatMatchesPredicate(int pageIndex, int pageSize, int startIndex, int endIndex)
        {
            // Arrange
            var filteredBlogs = dbContext.Blogs.Where(b => b.BlogId % 2 != 0)
                .ToList();
            var expectedBlogs = new List<Blog>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                expectedBlogs.Add(filteredBlogs[i]);
            }

            // Act
            var actualBlogs = repository.Find(b => b.BlogId % 2 != 0, pageIndex, pageSize);

            // Assert
            Assert.NotNull(actualBlogs);
            Assert.NotEmpty(actualBlogs);
            Assert.Equal(expectedBlogs, actualBlogs);
        }

        [Theory]
        [InlineData(2, 3, 3, 4)]
        [InlineData(1, 2, 0, 1)]
        public async Task FindAsync_ReturnsPagedItemsThatMatchesPredicate(int pageIndex, int pageSize, int startIndex, int endIndex)
        {
            // Arrange
            var filteredBlogs = dbContext.Blogs.Where(b => b.BlogId % 2 != 0)
                .ToList();
            var expectedBlogs = new List<Blog>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                expectedBlogs.Add(filteredBlogs[i]);
            }

            // Act
            var actualBlogs = await repository.FindAsync(b => b.BlogId % 2 != 0, pageIndex, pageSize).ConfigureAwait(false);

            // Assert
            Assert.NotNull(actualBlogs);
            Assert.NotEmpty(actualBlogs);
            Assert.Equal(expectedBlogs, actualBlogs);
        }
    }
}
