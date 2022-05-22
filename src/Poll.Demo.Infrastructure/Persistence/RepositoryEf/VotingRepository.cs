using System.Threading;
using System.Threading.Tasks;
using Poll.Demo.Application.Repository;
using Poll.Demo.Core.Entity;
using Poll.Demo.Infrastructure.Persistence.RepositoryEf.Configuration;

namespace Poll.Demo.Infrastructure.Persistence.RepositoryEf;

public class VotingRepository : IVotingRepository
{
    private readonly PollDbContext _dbContext;

    public VotingRepository(PollDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> Create(Voting voting, CancellationToken cancellationToken)
    {
        var savedEntity = await _dbContext.Set<Voting>().AddAsync(voting, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return savedEntity.Entity.Id;
    }

    public async Task<Voting> Get(int votingId, CancellationToken cancellationToken)
    {
        var set = _dbContext.Set<Voting>();
        var entity = await set.FindAsync(new object[] { votingId }, cancellationToken);
        return entity;
    }

    public async Task Update(Voting voting, CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}