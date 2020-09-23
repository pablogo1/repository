using Repository.Core;
using Repository.EF.Tests.Model;

#pragma warning disable CA1716 // Identifiers should not match keywords
namespace Repository.EF.Tests.Shared
#pragma warning restore CA1716 // Identifiers should not match keywords
{
    public interface IPostRepository : IRepository<Post, int>
    {
    }
}
