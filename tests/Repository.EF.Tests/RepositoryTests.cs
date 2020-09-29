using Repository.EF.Tests.Model;
using Repository.EF.Tests.Shared;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.EF.Tests
{
    public class RepositoryTests : IClassFixture<TestDatabaseFixture>
    {
        private readonly TestDbContext dbContext;
        private readonly IBlogRepository repository;

        public RepositoryTests(TestDatabaseFixture fixture)
        {
            if (fixture is null)
            {
                throw new System.ArgumentNullException(nameof(fixture));
            }

            var testDataContext = fixture.DataContextFactory.CreateTestDataContext();

            dbContext = testDataContext.DbContext;
            repository = testDataContext.BlogRepository;
        }

        [Fact]
        public void Add_ShouldAddANewEntityToTheUnderlyingDbContext()
        {
            var blog = new Blog { BlogId = 11, Url = "test.url" };
            Assert.False(dbContext.ChangeTracker.HasChanges());

            repository.Add(blog);

            Assert.True(dbContext.ChangeTracker.HasChanges());
        }

        [Fact]
        public async Task AddAsync_ShouldAddNewEntityToTheUnderlyingDbContext()
        {
            var blog = new Blog { BlogId = 11, Url = "test.url" };
            Assert.False(dbContext.ChangeTracker.HasChanges());

            await repository.AddAsync(blog).ConfigureAwait(false);

            Assert.True(dbContext.ChangeTracker.HasChanges());
        }

        [Fact]
        public void AddRange_ShouldAddNewEntitiesToTheUnderlyingDbContext()
        {
            var blogs = new List<Blog>()
            {
                new Blog { BlogId = 11, Url = "test.url" },
                new Blog { BlogId = 21, Url = "test 123.url" }
            };
            Assert.False(dbContext.ChangeTracker.HasChanges());

            repository.AddRange(blogs);

            var entries = dbContext.ChangeTracker.Entries<Blog>().Select(p => p.Entity);
            
            Assert.True(dbContext.ChangeTracker.HasChanges());
            Assert.Contains(blogs[0], entries);
            Assert.Contains(blogs[1], entries);
        }

        [Fact]
        public async Task AddRangeAsync_ShouldAddNewEntitiesToTheUndelyingDbContext()
        {
            var blogs = new List<Blog>()
            {
                new Blog { BlogId = 11, Url = "test.url" },
                new Blog { BlogId = 21, Url = "test 123.url" }
            };
            Assert.False(dbContext.ChangeTracker.HasChanges());

            await repository.AddRangeAsync(blogs).ConfigureAwait(false);

            var entries = dbContext.ChangeTracker.Entries<Blog>().Select(p => p.Entity);

            Assert.True(dbContext.ChangeTracker.HasChanges());
            Assert.Contains(blogs[0], entries);
            Assert.Contains(blogs[1], entries);
        }

        [Fact]
        public void Delete_ShouldCallUnderlyingDbContextRemoveMethod()
        {
            var blog = new Blog { BlogId = 1, Url = "test" };
            Assert.False(dbContext.ChangeTracker.HasChanges());

            repository.Delete(blog);

            var entries = dbContext.ChangeTracker.Entries<Blog>().Select(p => p.Entity);

            Assert.True(dbContext.ChangeTracker.HasChanges());
            Assert.Contains(blog, entries);
        }

        [Fact]
        public void DeleteRange_ShouldCallUnderlyingDbContextRemoveRangeMethod()
        {
            var blogs = new List<Blog>() 
            {
                new Blog { BlogId = 1, Url = "test" },
                new Blog { BlogId = 2, Url = "test 123" }
            };
            Assert.False(dbContext.ChangeTracker.HasChanges());
            
            repository.DeleteRange(blogs);

            var entries = dbContext.ChangeTracker.Entries<Blog>().Select(p => p.Entity);

            Assert.True(dbContext.ChangeTracker.HasChanges());
            Assert.Contains(blogs[0], entries);
            Assert.Contains(blogs[1], entries);
        }
    }
}
