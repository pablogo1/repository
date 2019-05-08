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

        [Fact]
        public void All_ReturnsElementsFrom0To4_WhenPageIndexIsZeroAndPageSizeIs5()
        {
            //Arrange
            const int pageIndex = 0;
            const int pageSize = 5;
            const int startIndex = 0;
            const int endIndex = 4;

            //Act
            TestPaging(pageIndex, pageSize, startIndex, endIndex);
        }

        [Fact]
        public void All_ReturnsElementsFrom4To9_WhenPageIndexIsOneAndPageSizeIs5()
        {
            //Arrange
            const int pageIndex = 1;
            const int pageSize = 5;
            const int startIndex = 4;
            const int endIndex = 9;

            //Act
            TestPaging(pageIndex, pageSize, startIndex, endIndex);
        }

        private void TestPaging(int pageIndex, int pageSize, int index0, int index1)
        {
            var blogs = dbContext.Blogs.ToList();
            var expectedBlogs = new List<Blog>();
            for (int i = index0; i <= index1; i++)
            {
                expectedBlogs.Add(blogs[i]);
            }

            var actualBlogs = repository.All(pageIndex, pageSize);

            Assert.NotNull(actualBlogs);
            Assert.NotEmpty(actualBlogs);
            Assert.Equal(expectedBlogs, actualBlogs);
        }
    }
}
