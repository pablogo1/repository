using Microsoft.EntityFrameworkCore;
using Repository.EF.Tests.Model;
using System.Linq;

namespace Repository.EF.Tests.Shared
{
    public class InMemoryDbContextFactory
    {
        public TestDbContext CreateDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            var dbContext = new TestDbContext(options);

            SetupBaseData(dbContext);

            return dbContext;
        }

        private static void SetupBaseData(TestDbContext dbContext)
        {
            if (!dbContext.Blogs.Any())
            {
                dbContext.Blogs.AddRange(new Blog[] {
                    new Blog { BlogId = 1, Url = "test" },
                    new Blog { BlogId = 2, Url = "test 2" },
                    new Blog { BlogId = 3, Url = "test 3" },
                    new Blog { BlogId = 4, Url = "test 4" },
                    new Blog { BlogId = 5, Url = "test 5" },
                    new Blog { BlogId = 6, Url = "test 6" },
                    new Blog { BlogId = 7, Url = "test 7" },
                    new Blog { BlogId = 8, Url = "test 8" },
                    new Blog { BlogId = 9, Url = "test 9" },
                    new Blog { BlogId = 10, Url = "test 10" }
                });
            }

            if (!dbContext.Posts.Any())
            {
                dbContext.Posts.AddRange(new Post[] {
                    new Post { PostId = 1, BlogId = 1, Title = "Blog 1 Post 1" },
                    new Post { PostId = 2, BlogId = 1, Title = "Blog 1 Post 2" },
                    new Post { PostId = 3, BlogId = 1, Title = "Blog 1 Post 3" },
                    new Post { PostId = 4, BlogId = 2, Title = "Blog 2 Post 1" },
                    new Post { PostId = 5, BlogId = 2, Title = "Blog 2 Post 2" },
                    new Post { PostId = 6, BlogId = 2, Title = "Blog 2 Post 3" }
                });
            }

            dbContext.SaveChanges();
        }
    }
}
