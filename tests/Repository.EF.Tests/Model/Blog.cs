using System.Collections.Generic;

namespace Repository.EF.Tests.Model
{
    public class Blog
    {
        public Blog()
        {
            Posts = new HashSet<Post>();
        }
        
        public int BlogId { get; set; }
#pragma warning disable CA1056 // URI-like properties should not be strings
        public string Url { get; set; }
#pragma warning restore CA1056 // URI-like properties should not be strings

        public ICollection<Post> Posts { get; }
    }
}
