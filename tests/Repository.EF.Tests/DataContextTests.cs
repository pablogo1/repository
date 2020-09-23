using Repository.EF.Tests.Shared;
using Moq;
using Xunit;
using Repository.EF.Tests.Model;
using System.Threading.Tasks;
using System.Threading;

namespace Repository.EF.Tests
{
    public class DataContextTests
    {
        private readonly TestDataContext testDataContext;
        private readonly Mock<TestDbContext> mockDbContext;

        public DataContextTests()
        {
            mockDbContext = new Mock<TestDbContext>();
            testDataContext = new TestDataContext(mockDbContext.Object);
        }

        [Fact]
        public void Commit_ShouldCallUnderlyingDbContextSaveChanges()
        {
            testDataContext.Commit();
            mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public async Task CommitAsync_ShouldCallUnderlyingDbContextSaveChangesAsync()
        {
            await testDataContext.CommitAsync().ConfigureAwait(false);

            mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
