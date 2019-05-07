using Repository.EF.Tests.Model;
using Repository.EF.Tests.Shared;
using Xunit;
using System.Linq;

namespace Repository.EF.Tests
{
    public class ReadOnlyRepositoryTests : IClassFixture<TestDatabaseFixture>
    {
        private readonly TestDbContext dbContext;
        private readonly BlogRepository repository;

        public ReadOnlyRepositoryTests()
        {
            var dbContextFactory = new InMemoryDbContextFactory();
            dbContext = dbContextFactory.CreateDbContext();
            repository = new BlogRepository(dbContext);

            SetupDbContext();
        }

        [Fact]
        public void GetById_ReturnsCorrectItem()
        {
            //Arrange
            const int blogId = 2;

            //Act
            var actual = repository.GetById(2);

            //Assert
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
        public void All_ReturnsElementsFrom0To4_WhenPageIndexIsZeroAndPageSizeIs5()
        {
            //Arrange
            const int pageIndex = 0;
            const int pageSize = 5;
            var blogs = dbContext.Blogs.ToList();
            var expectedItems = blogs.Take(pageSize);

            //Act
            var items = repository.All(pageIndex, pageSize);

            //Assert
            Assert.NotNull(items);
            Assert.NotEmpty(items);
            Assert.Equal(expectedItems, items);
            
        }
    }
}