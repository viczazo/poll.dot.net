using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Poll.Demo.Application.Cqrs.Abstractions;
using Poll.Demo.Application.Cqrs.Command;
using Poll.Demo.Application.Model;
using Poll.Demo.Application.Repository;
using Poll.Demo.Core.Exceptions;

namespace Poll.Demo.Application.Cqrs.CommandHandler
{
    public class CreateVoteHandler : IRequestHandler<CreateVoteCommand, CreateVoteResponse>
    {
        private readonly IVoterRepository _voterRepository;
        private readonly ILogger<CreateVoteHandler> _logger;

        public CreateVoteHandler(IVoterRepository voterRepository,
            ILogger<CreateVoteHandler> logger)
        {
            _voterRepository = voterRepository;
            _logger = logger;
        }

        public async Task<CreateVoteResponse> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var voter = await _voterRepository.Get(request.VoterId, request.VotingId, cancellationToken);

                if (voter == null)
                {
                    return CreateVoteResponse.VoteResponseError("You are not part of this voting",
                        ErrorType.Validation);
                }
                
                voter.DoVote(request.Type);
                await _voterRepository.Update(voter, cancellationToken);
                _logger.LogInformation("Vote with voting Id {voting.id} for voter with Id:{voter.id} created",
                    request.VotingId,
                    request.VoterId);

                return new CreateVoteResponse
                {
                    ActionResult = new AppActionResult<int>
                    {
                        IsSuccesfull = true,
                        Data = voter.Vote.Id
                    }
                };
            }
            catch (EntityValidationException e)
            {
                _logger.LogError(e, "Failed to create vote : {message}", e.Message);
                return CreateVoteResponse.VoteResponseError(e.Message, ErrorType.Validation);
            }
        }
    }
}