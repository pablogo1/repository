using System;
using Microsoft.EntityFrameworkCore;
using Repository.EF.Tests.Shared;
using Moq;
using Xunit;
using Repository.EF.Tests.Model;
using System.Threading.Tasks;
using System.Threading;

namespace Repository.EF.Tests
{
    public class DataContextTests //: IClassFixture<TestDatabaseFixture>
    {
        private readonly TestDataContext testDataContext;
        private readonly Mock<TestDbContext> mockDbContext;

        public DataContextTests()
        {
            mockDbContext = new Mock<TestDbContext>();
            testDataContext = new TestDataContext(mockDbContext.Object);
        }

        // public DataContextTests(TestDatabaseFixture fixture)
        // {
        //     testDataContext = fixture.DataContext;
        // }

        [Fact]
        public void Commit_ShouldCallUnderlyingDbContextSaveChanges()
        {
            testDataContext.Commit();
            mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
            // var blog = new Model.Blog() { BlogId = 1, Url = "test"};
            // testDataContext.BlogRepository.Add(blog);

            // testDataContext.Commit();

            // var testBlog = testDataContext.BlogRepository.GetById(1);

            // Assert.NotNull(testBlog);
            // Assert.Equal(blog, testBlog);
            
        }

        [Fact]
        public async Task CommitAsync_ShouldCallUnderlyingDbContextSaveChangesAsync()
        {
            await testDataContext.CommitAsync();

            mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
