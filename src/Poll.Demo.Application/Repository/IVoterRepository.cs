using System.Threading;
using System.Threading.Tasks;
using Poll.Demo.Core.Entity;

namespace Poll.Demo.Application.Repository
{
    public interface IVoterRepository
    {
        public Task<Voter> Get(int voterId, int votingId, CancellationToken cancellationToken);
        public Task<bool> Exists(int nationalIdentity, int votingId, CancellationToken cancellationToken);
        public Task<int> Update(Voter voter, CancellationToken cancellationToken);
        Task<int> Create(Voter voter, CancellationToken cancellationToken);
    }
}