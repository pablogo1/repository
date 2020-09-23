using Repository.EF.Tests.Model;
using Repository.EF.Tests.Shared;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;

namespace Repository.EF.Tests
{
    public class ReadOnlyRepositoryTests
    {
        private readonly IBlogRepository repository;
        private readonly Mock<TestDbContext> mockDbContext;

        public ReadOnlyRepositoryTests()
        {
            mockDbContext = new Mock<TestDbContext>();
            repository = new BlogRepository(mockDbContext.Object);

            mockDbContext.Setup(p => p.Blogs.Find(It.Is<int>(id => id == 2)))
                .Returns(new Blog
                {
                    BlogId = 2,
                    Url = "test.url"
                });
        }

        [Fact]
        public void GetById_ReturnsCorrectItem()
        {
            //Arrange
            const int blogId = 2;

            //Act
            var actual = repository.GetById(blogId);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(blogId, actual.BlogId);
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
        public async Task AllAsync_CallsUnderlyingDbContext()
        {
            var items = await repository.AllAsync().ConfigureAwait(false);

            mockDbContext.Verify(m => m.Blogs.All(null), Times.Once);
        }

        [Fact]
        public void All_CallsUnderlyingDbContext()
        {
            var items = repository.All();

            mockDbContext.Verify(m => m.Blogs.All(null), Times.Once);
        }

        //[Theory]
        //[InlineData(1, 5, 0, 4)]
        //[InlineData(2, 5, 5, 9)]
        //[InlineData(3, 2, 4, 5)]
        //public void All_ReturnsCorrectElements_WhenPageIndexAndPageSizeAreProvided(int pageIndex, int pageSize, int startIndex, int endIndex)
        //{
        //    // Arrange
        //    var blogs = dbContext.Blogs.ToList();
        //    var expectedBlogs = new List<Blog>();
        //    for (int i = startIndex; i <= endIndex; i++)
        //    {
        //        expectedBlogs.Add(blogs[i]);
        //    }

        //    // Act
        //    var actualBlogs = repository.All(pageIndex, pageSize);

        //    // Assert
        //    Assert.NotNull(actualBlogs);
        //    Assert.NotEmpty(actualBlogs);
        //    Assert.Equal(expectedBlogs, actualBlogs);
        //}

        //[Theory]
        //[InlineData(1, 5, 0, 4)]
        //[InlineData(2, 5, 5, 9)]
        //[InlineData(3, 2, 4, 5)]
        //public async Task AllAsync_ReturnsCorrectElements_WhenPageIndexAndPageSizeAreProvided(int pageIndex, int pageSize, int startIndex, int endIndex)
        //{
        //    // Arrange
        //    var blogs = dbContext.Blogs.ToList();
        //    var expectedBlogs = new List<Blog>();
        //    for (int i = startIndex; i <= endIndex; i++)
        //    {
        //        expectedBlogs.Add(blogs[i]);
        //    }

        //    // Act
        //    var actualBlogs = await repository.AllAsync(pageIndex, pageSize).ConfigureAwait(false);

        //    // Assert
        //    Assert.NotNull(actualBlogs);
        //    Assert.NotEmpty(actualBlogs);
        //    Assert.Equal(expectedBlogs, actualBlogs);
        //}

        //[Fact]
        //public void Find_ReturnsAllItemsThatMatchesPredicate()
        //{
        //    //Arrange
        //    var expectedBlogs = dbContext.Blogs.Where(b => b.BlogId % 2 != 0)
        //        .ToList();
        //    var expectedItemCount = expectedBlogs.Count;

        //    //Act
        //    var actualBlogs = repository.Find(b => b.BlogId % 2 != 0);

        //    //Assert
        //    Assert.NotNull(actualBlogs);
        //    Assert.NotEmpty(actualBlogs);
        //    Assert.Equal(expectedBlogs, actualBlogs);
        //    Assert.Equal(expectedItemCount, actualBlogs.ToList().Count);
        //}

        //[Fact]
        //public async Task FindAsync_ReturnsAllItemsThatMatchesPredicate()
        //{
        //    //Arrange
        //    var expectedBlogs = dbContext.Blogs.Where(b => b.BlogId % 2 != 0)
        //        .ToList();
        //    var expectedItemCount = expectedBlogs.Count;

        //    //Act
        //    var actualBlogs = await repository.FindAsync(b => b.BlogId % 2 != 0).ConfigureAwait(false);

        //    //Assert
        //    Assert.NotNull(actualBlogs);
        //    Assert.NotEmpty(actualBlogs);
        //    Assert.Equal(expectedBlogs, actualBlogs);
        //    Assert.Equal(expectedItemCount, actualBlogs.ToList().Count);
        //}

        //[Theory]
        //[InlineData(2, 3, 3, 4)]
        //[InlineData(1, 2, 0, 1)]
        //public void Find_ReturnsPagedItemsThatMatchesPredicate(int pageIndex, int pageSize, int startIndex, int endIndex)
        //{
        //    // Arrange
        //    var filteredBlogs = dbContext.Blogs.Where(b => b.BlogId % 2 != 0)
        //        .ToList();
        //    var expectedBlogs = new List<Blog>();
        //    for (int i = startIndex; i <= endIndex; i++)
        //    {
        //        expectedBlogs.Add(filteredBlogs[i]);
        //    }

        //    // Act
        //    var actualBlogs = repository.Find(b => b.BlogId % 2 != 0, pageIndex, pageSize);

        //    // Assert
        //    Assert.NotNull(actualBlogs);
        //    Assert.NotEmpty(actualBlogs);
        //    Assert.Equal(expectedBlogs, actualBlogs);
        //}

        //[Theory]
        //[InlineData(2, 3, 3, 4)]
        //[InlineData(1, 2, 0, 1)]
        //public async Task FindAsync_ReturnsPagedItemsThatMatchesPredicate(int pageIndex, int pageSize, int startIndex, int endIndex)
        //{
        //    // Arrange
        //    var filteredBlogs = dbContext.Blogs.Where(b => b.BlogId % 2 != 0)
        //        .ToList();
        //    var expectedBlogs = new List<Blog>();
        //    for (int i = startIndex; i <= endIndex; i++)
        //    {
        //        expectedBlogs.Add(filteredBlogs[i]);
        //    }

        //    // Act
        //    var actualBlogs = await repository.FindAsync(b => b.BlogId % 2 != 0, pageIndex, pageSize).ConfigureAwait(false);

        //    // Assert
        //    Assert.NotNull(actualBlogs);
        //    Assert.NotEmpty(actualBlogs);
        //    Assert.Equal(expectedBlogs, actualBlogs);
        //}
    }
}
