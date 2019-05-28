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
        private readonly BlogRepositoryAsync asyncRepository;

        public RepositoryTests()
        {
            mockDbContext = new Mock<TestDbContext>();
            repository = new BlogRepository(mockDbContext.Object);
            asyncRepository = new BlogRepositoryAsync(mockDbContext.Object);
        }

        [Fact]
        public async Task Add_ShouldCallUnderlyingDbContextAddMethod()
        {
            var blog = new Blog { BlogId = 1, Url = "test" };
            repository.Add(blog);
            await asyncRepository.AddAsync(blog);

            mockDbContext.Verify(m => m.Add(It.IsAny<Blog>()), Times.Once);
            mockDbContext.Verify(m => m.AddAsync(It.IsAny<Blog>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddRange_ShouldCallUnderlyingDbContextAddRangeMethod()
        {
            var blogs = new HashSet<Blog>() 
            {
                new Blog { BlogId = 1, Url = "test" },
                new Blog { BlogId = 2, Url = "test 123" }
            };
            
            repository.AddRange(blogs);
            await asyncRepository.AddRangeAsync(blogs);

            mockDbContext.Verify(m => m.AddRange(It.IsAny<IEnumerable<Blog>>()), Times.Once);
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