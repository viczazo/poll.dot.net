using System.Threading;
using System.Threading.Tasks;
using Poll.Demo.Application.Model.Voting;

namespace Poll.Demo.Application.Repository
{
    public interface IReadVotingRepository
    {
        public Task<VotingResults> GetResults(int votingId, CancellationToken cancellationToken);
    }
}