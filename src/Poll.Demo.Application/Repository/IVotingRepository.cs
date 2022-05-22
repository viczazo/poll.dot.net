using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Poll.Demo.Application.Model.Voting;
using Poll.Demo.Core.Entity;

namespace Poll.Demo.Application.Repository
{
    public interface IVotingRepository
    {
        public Task<int> Create(Voting voting, CancellationToken cancellationToken);
        public Task<Voting> Get(int votingId, CancellationToken cancellationToken);
        public Task Update(Voting voting, CancellationToken cancellationToken);
        
    }
}
