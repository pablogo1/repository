using Repository.EF;
using Repository.EF.Tests.Model;
using Repository.EF.Tests.Shared;
using Xunit;
using Moq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace Repository.EF.Tests
{
    public class RepositoryTests
    {
        private readonly Mock<TestDbContext> mockDbContext;
        private readonly BlogRepository repository;

        public RepositoryTests()
        {
            mockDbContext = new Mock<TestDbContext>();
            repository = new BlogRepository(mockDbContext.Object);
        }

        [Fact]
        public void Add_ShouldCallUnderlyingDbContextAddMethod()
        {
            var blog = new Blog { BlogId = 1, Url = "test" };
            repository.Add(blog);

            mockDbContext.Verify(m => m.Add(It.IsAny<Blog>()), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldCallUnderlyingDbContextAddAsyncMethod()
        {
            var blog = new Blog { BlogId = 1, Url = "test" };
            await repository.AddAsync(blog);

            mockDbContext.Verify(m => m.AddAsync(It.IsAny<Blog>(), It.IsAny<CancellationToken>()), Times.Once);
        }


        [Fact]
        public void AddRange_ShouldCallUnderlyingDbContextAddRangeMethod()
        {
            var blogs = new HashSet<Blog>()
            {
                new Blog { BlogId = 1, Url = "test" },
                new Blog { BlogId = 2, Url = "test 123" }
            };

            repository.AddRange(blogs);

            mockDbContext.Verify(m => m.AddRange(It.IsAny<IEnumerable<Blog>>()), Times.Once);
        }

        [Fact]
        public async Task AddRangeAsync_ShouldCallUnderlyingDbContextAddRangeAsyncMethod()
        {
            var blogs = new HashSet<Blog>()
            {
                new Blog { BlogId = 1, Url = "test" },
                new Blog { BlogId = 2, Url = "test 123" }
            };

            await repository.AddRangeAsync(blogs);

            mockDbContext.Verify(m => m.AddRangeAsync(It.IsAny<IEnumerable<Blog>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void Delete_ShouldCallUnderlyingDbContextRemoveMethod()
        {
            var blog = new Blog { BlogId = 1, Url = "test" };

            repository.Delete(blog);

            mockDbContext.Verify(m => m.Remove(It.IsAny<Blog>()), Times.Once);
        }

        [Fact]
        public void DeleteRange_ShouldCallUnderlyingDbContextRemoveRangeMethod()
        {
            var blogs = new HashSet<Blog>() 
            {
                new Blog { BlogId = 1, Url = "test" },
                new Blog { BlogId = 2, Url = "test 123" }
            };
            
            repository.DeleteRange(blogs);

            mockDbContext.Verify(m => m.RemoveRange(It.IsAny<IEnumerable<Blog>>()), Times.Once);
        }
    }
}