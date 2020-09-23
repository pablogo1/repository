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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Entity<Blog>()
                .HasKey(m => m.BlogId);
            builder.Entity<Blog>()
                .Property(b => b.Url)
                .IsRequired();
            builder.Entity<Blog>()
                .HasMany<Post>(b => b.Posts)
                .WithOne(p => p.Blog)
                .HasForeignKey(p => p.BlogId);
            builder.Entity<Post>()
                .HasKey(m => m.PostId);
            builder.Entity<Post>()
                .HasOne<Blog>(m => m.Blog);
        }
    }
}
