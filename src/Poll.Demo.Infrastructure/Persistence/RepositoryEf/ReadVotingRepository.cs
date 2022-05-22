using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Poll.Demo.Application.Model.Voting;
using Poll.Demo.Application.Repository;
using Poll.Demo.Core.Entity;
using Poll.Demo.Infrastructure.Persistence.RepositoryEf.Configuration;

namespace Poll.Demo.Infrastructure.Persistence.RepositoryEf;

public class ReadVotingRepository : IReadVotingRepository
{
    private readonly PollDbContext _dbContext;

    public ReadVotingRepository(PollDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<VotingResults> GetResults(int votingId, CancellationToken cancellationToken)
    {
        var voting = _dbContext.Set<Voting>()
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == votingId && x.State == VotingState.Closed);
        if (voting == null)
            return null;
        
        var queryable = _dbContext.Set<Vote>().Where(x => x.Voting.Id.Equals(votingId));

        var positives = await queryable
            .AsNoTracking()
            .Where(x => x.Type == VoteType.Positive)
            .CountAsync(cancellationToken);

        var negatives = await queryable
            .AsNoTracking()
            .Where(x => x.Type == VoteType.Negative)
            .CountAsync(cancellationToken);
        
        return new VotingResults
        {
            VotingId = votingId,
            Positives = positives,
            Negatives = negatives
        };
    }
}