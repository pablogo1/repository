using Repository.EF.Tests.Model;
using Repository.EF.Tests.Shared;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Repository.EF.Tests
{
    public class ReadOnlyRepositoryTests : IClassFixture<TestDatabaseFixture>
    {
        private readonly IBlogRepository repository;
        private readonly TestDbContext dbContext;

        public ReadOnlyRepositoryTests(TestDatabaseFixture testDatabaseFixture)
        {
            dbContext = testDatabaseFixture.DbContext;
            repository = testDatabaseFixture.BlogRepository;
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

        [Fact]
        public void All_PageIndexShouldBeGreaterThanZero()
        {
            Assert.NotNull(repository.All(1, 5));

            Assert.Throws<ArgumentOutOfRangeException>(() => repository.All(0, 5));
            Assert.Throws<ArgumentOutOfRangeException>(() => repository.All(-1, 5));
        }

        [Fact]
        public void All_PageSizeShouldBeGreaterOrEqualToOne()
        {
            Assert.NotNull(repository.All(1, 1));
            Assert.NotNull(repository.All(1, 5));

            Assert.Throws<ArgumentOutOfRangeException>(() => repository.All(1, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => repository.All(1, -5));
        }

        [Fact]
        public void Find_PageIndexShouldBeGreaterThanZero()
        {
            Assert.NotNull(repository.Find(b => b.BlogId % 2 != 0, 1, 5));

            Assert.Throws<ArgumentOutOfRangeException>(() => repository.Find(b => b.BlogId % 2 != 0, 0, 5));
            Assert.Throws<ArgumentOutOfRangeException>(() => repository.Find(b => b.BlogId % 2 != 0, -1, 5));
        }

        [Fact]
        public void Find_PageSizeShouldBeGreaterOrEqualToOne()
        {
            Assert.NotNull(repository.Find(b => b.BlogId % 2 != 0, 1, 1));
            Assert.NotNull(repository.Find(b => b.BlogId % 2 != 0, 1, 5));

            Assert.Throws<ArgumentOutOfRangeException>(() => repository.Find(b => b.BlogId % 2 != 0, 1, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => repository.Find(b => b.BlogId % 2 != 0, 1, -5));
        }
    }
}
