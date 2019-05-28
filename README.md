# Repository
Repository and Unit of Work patterns implemented in C#. This has the following benefits:
* **Decoupling:** Avoids having your OR/M provider all over your application code.
* **Query reuse:** Avoid having your LINQ queries all over your code. Put the query code on your repository, use it whereever you want.
* **Single-responsability:** Your repository is the sole responsible to access and query your data. Stick to that principle, avoid headaches down the road.

## Getting Started
### Installation
Install the required packages (this will install the Entity Framework version):
```
$ dotnet add package PG.Repository.EF
```
 
### Usage
This steps describe using the `PG.Repository.EF` package.

1. Define your model:
```csharp
public class Blog
{
  public int BlogId { get; set; }
  public int Name { get; set; }

  public HashSet<Post> Posts { get; set; }
}

public class Post 
{
  public int PostId { get; set; }
  public int BlogId { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }

  public Blog Blog { get; set; }
}
```

2. Define repository interface:
```csharp
using PG.Repository.Core;

public interface IBlogRepository : IRepository<Blog, int>
{

}

public interface IPostRepository : IRepository<Post, int>
{
  /* Custom query method with pageIndex and pageSize (to be deprecated in favor of PagingOptions) */
  IEnumerable<Post> FindByTitle(string titleContains, int pageIndex, int pageSize);
  /* Custom query method with PagingOptions */
  IEnumerable<Post> FindByTitle(string titleContains, PagingOptions pageOptions);
}
```

3. Implement `DataContext`:
```csharp
using System;
using PG.Repository.EF;

public interface IMyBloggingDataContext : IDataContext
{
  public IBlogRepository BlogRepository { get; }
  public IPostRepository PostRepository { get; }
}

public class MyBloggingDataContext : DataContext, IMyBloggingDataContext
{
  public MyBloggingDataContext(YourDbContext dbContext, 
    IBlogRepository blogRepository, 
    IPostRepository postRepository) 
    : base(dbContext)
  {
    BlogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
    PostRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
  }

  public IBlogRepository BlogRepository { get; }
  public IPostRepository PostRepository { get; }
}
```

4. Implement `IBlogRepository` and `IPostRepository`:
```csharp
using PG.Repository.EF;

public class BlogRepository : Repository<Blog, int>, IBlogRepository
{
  public class BlogRepository(MyBloggingDataContext dbContext)
    : base(dbContext)
  {}
}

public class PostRepository : Repository<Post, int>, IPostRepository
{
  public class PostRepository(MyBloggingContext dbContext)
    : base(dbContext)
  {
    DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
  }

  /* Access to EF DbContext to allow query building. */
  private new MyBloggingContext DbContext { get; }

  /* Custom query method with pageIndex and pageSize (to be deprecated in favor of PagingOptions) */
  IEnumerable<Post> FindByTitle(string titleContains, int pageIndex, int pageSize)
  {
    var pagingOptions = new PagingOptions 
    {
      PageIndex = pageIndex,
      PageSize = pageSize
    };

    return FindByTitle(titleContains, pagingOptions);
  }

  /* Custom query method with PagingOptions */
  IEnumerable<Post> FindByTitle(string titleContains, PagingOptions pageOptions)
  {
    var query = DbContext.Posts
      .Where(p => p.Title.Contains(titleContains))
      .Page(pageOptions);

    return query.ToList();
  }
}
```
