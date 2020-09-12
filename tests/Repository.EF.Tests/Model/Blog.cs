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
        public string Url { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
