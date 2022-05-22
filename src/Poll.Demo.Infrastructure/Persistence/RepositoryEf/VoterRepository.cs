using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Poll.Demo.Application.Repository;
using Poll.Demo.Core.Entity;
using Poll.Demo.Infrastructure.Persistence.RepositoryEf.Configuration;

namespace Poll.Demo.Infrastructure.Persistence.RepositoryEf;

public class VoterRepository : IVoterRepository
{
    private readonly PollDbContext _dbContext;

    public VoterRepository(PollDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Voter> Get(int voterId, int votingId, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Voter>()
            .Include(x => x.Vote)
            .Include(x => x.Voting)
            .FirstOrDefaultAsync( x=> x.Id == voterId && x.Voting.Id == votingId , cancellationToken);
    }

    public Task<bool> Exists(int nationalIdentity, int votingId, CancellationToken cancellationToken)
    {
        return _dbContext.Set<Voter>().AnyAsync(
            x => x.NationalIdentity == nationalIdentity
            && x.Voting.Id == votingId, cancellationToken);
    }

    public async Task<int> Create(Voter voter, CancellationToken cancellationToken)
    {
        var savedEntity = await _dbContext.Set<Voter>().AddAsync(voter, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return savedEntity.Entity.Id;
    }

    public async Task<int> Update(Voter voter, CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}