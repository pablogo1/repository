using System.Threading.Tasks;

namespace Repository.Core
{
    public interface IDbContext
    {
        void Commit();
        Task CommitAsync();
    }
}