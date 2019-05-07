using System;
using Microsoft.EntityFrameworkCore;

namespace Repository.EF.Tests.Model
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {

        }

        protected internal TestDbContext()
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
