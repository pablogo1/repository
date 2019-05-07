using Repository.EF;
using Repository.EF.Tests.Model;
using Repository.EF.Tests.Shared;
using Xunit;
using Moq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
            repository.Add(new Blog { BlogId = 1, Url = "test" });

            mockDbContext.Verify(m => m.Add(It.IsAny<Blog>()), Times.Once);
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