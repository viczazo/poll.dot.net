using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Poll.Demo.Application.Cqrs.Abstractions;
using Poll.Demo.Application.Cqrs.Command;
using Poll.Demo.Application.Model;
using Poll.Demo.Application.Repository;

namespace Poll.Demo.Application.Cqrs.CommandHandler
{
    public class ChangeVotingStateHandler : IRequestHandler<ChangeVotingStateCommand, AppActionResult>
    {
        private readonly IVotingRepository _votingRepository;
        private readonly ILogger<ChangeVotingStateHandler> _logger;

        public ChangeVotingStateHandler(IVotingRepository votingRepository, ILogger<ChangeVotingStateHandler> logger)
        {
            _votingRepository = votingRepository;
            _logger = logger;
        }
        public async Task<AppActionResult> Handle(ChangeVotingStateCommand request, CancellationToken cancellationToken)
        {
            var voting = await _votingRepository.Get(request.VotingId, cancellationToken);
            try
            {
                voting.ChangeState(request.VotingState);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error during change voting State from {voting.state} to {voting.state}", voting.State, request.VotingState);
                return new AppActionResult
                {
                    IsSuccesfull = false,
                    ErrorMessage = $"Change from state {voting.State} to {request.VotingState} is not valid",
                    ErrorType = ErrorType.Validation
                };
            }
            
            await _votingRepository.Update(voting, cancellationToken);
            _logger.LogInformation("Voting with Id: {voting.id} opened", request.VotingId);
            return new AppActionResult();
        }
    }
}