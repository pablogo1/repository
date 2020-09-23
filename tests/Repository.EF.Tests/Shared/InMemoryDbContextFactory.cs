using Microsoft.EntityFrameworkCore;
using Repository.EF.Tests.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.EF.Tests.Shared
{
    public class InMemoryDbContextFactory
    {
        private static readonly object lockObj = new object();
        private static bool databaseInitialized;

        public InMemoryDbContextFactory()
        {
            Seed();
        }

        public TestDbContext CreateDbContext()
        {
            return CreateDbContext("test");
        }

        public TestDbContext CreateDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            var dbContext = new TestDbContext(options);

            return dbContext;
        }

        private void Seed()
        {
            lock (lockObj) 
            {
                if (databaseInitialized) return;

                using (var dbContext = CreateDbContext())
                {
                    dbContext.Database.EnsureDeleted();
                    dbContext.Database.EnsureCreated();

                    if (!dbContext.Blogs.Any())
                    {
                        var blog1 = new Blog
                        {
                            BlogId = 1,
                            Url = "test"
                        };
                        blog1.Posts.Add(new Post { PostId = 1, BlogId = 1, Title = "Blog 1 Post 1" });
                        blog1.Posts.Add(new Post { PostId = 2, BlogId = 1, Title = "Blog 1 Post 2" });
                        blog1.Posts.Add(new Post { PostId = 3, BlogId = 1, Title = "Blog 1 Post 3" });

                        dbContext.Blogs.AddRange(new Blog[] {
                            blog1,
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
                            new Post { PostId = 4, BlogId = 2, Title = "Blog 2 Post 1" },
                            new Post { PostId = 5, BlogId = 2, Title = "Blog 2 Post 2" },
                            new Post { PostId = 6, BlogId = 2, Title = "Blog 2 Post 3" }
                        });
                    }

                    dbContext.SaveChanges();
                }

                databaseInitialized = true;
            }
        }
    }
}
