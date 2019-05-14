using System.Linq;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using Repository.EF.Tests.Model;

namespace Repository.EF.Tests.Shared
{
    public interface IPostRepository : IRepository<Post, int>
    {
    }
}
