using Repository.EF.Tests.Model;

namespace Repository.EF.Tests.Shared
{
    public class TestDatabaseFixture
    {
        public TestDatabaseFixture()
        {
            var dbContextFactory = new InMemoryDbContextFactory();
            DbContext = dbContextFactory.CreateDbContext();
            BlogRepository = new BlogRepository(DbContext);
            PostRepository = new PostRepository(DbContext);

            DataContext = new TestDataContext(DbContext, BlogRepository, PostRepository);

            SetupBaseData();
        }

        private void SetupBaseData()
        {
            // var blog1 = new Blog { BlogId = 1, Url = "test" };
            // var blog2 = new Blog { BlogId = 2, Url = "test2" };
            // var post11 = new Post { PostId = 1, BlogId = 1, Title = "Blog 1 Post 1" };
            // var post12 = new Post { PostId = 2, BlogId = 1, Title = "Blog 1 Post 2" };
            // var post13 = new Post { PostId = 3, BlogId = 1, Title = "Blog 1 Post 3" };
            // var post21 = new Post { PostId = 4, BlogId = 2, Title = "Blog 2 Post 1" };
            // var post22 = new Post { PostId = 5, BlogId = 2, Title = "Blog 2 Post 2" };
            // var post23 = new Post { PostId = 6, BlogId = 2, Title = "Blog 2 Post 3" };

            DbContext.Blogs.AddRange(new Blog[] {
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

            DbContext.Posts.AddRange(new Post[] {
                new Post { PostId = 1, BlogId = 1, Title = "Blog 1 Post 1" },
                new Post { PostId = 2, BlogId = 1, Title = "Blog 1 Post 2" },
                new Post { PostId = 3, BlogId = 1, Title = "Blog 1 Post 3" },
                new Post { PostId = 4, BlogId = 2, Title = "Blog 2 Post 1" },
                new Post { PostId = 5, BlogId = 2, Title = "Blog 2 Post 2" },
                new Post { PostId = 6, BlogId = 2, Title = "Blog 2 Post 3" }
            });
            // blog1.Posts.Add(post11);
            // blog1.Posts.Add(post12);
            // blog1.Posts.Add(post13);
            // blog2.Posts.Add(post21);
            // blog2.Posts.Add(post22);
            // blog2.Posts.Add(post23);

            // dbContext.Blogs.AddRange(new Blog[] { blog1, blog2 });
            // dbContext.Posts.AddRange(new Post[] { post11, post12, post13, post21,
            //     post22, post23});

            DbContext.SaveChanges();
        }

        public TestDataContext DataContext { get; }
        public IBlogRepository BlogRepository { get; }
        public IPostRepository PostRepository { get; }

        public TestDbContext DbContext { get; }
     }
}
