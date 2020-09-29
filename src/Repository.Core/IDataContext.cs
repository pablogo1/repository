using System.Threading;
using System.Threading.Tasks;

namespace Repository.Core
{
    public interface IDataContext
    {
        void Commit();
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
