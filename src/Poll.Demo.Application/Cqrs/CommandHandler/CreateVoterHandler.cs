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
    public class CreateVoterHandler : IRequestHandler<CreateVoterCommand, CreateVoterResponse>
    {
        private readonly IVoterRepository _voterRepository;
        private readonly IVotingRepository _votingRepository;
        private readonly ILogger<CreateVoterHandler> _logger;

        public CreateVoterHandler(IVoterRepository voterRepository,
            IVotingRepository votingRepository,
            ILogger<CreateVoterHandler> logger)
        {
            _voterRepository = voterRepository;
            _votingRepository = votingRepository;

            _logger = logger;
        }

        public async Task<CreateVoterResponse> Handle(CreateVoterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _voterRepository.Exists(request.NationalityId, request.VotingId, cancellationToken))
                {
                    return CreateVoterResponse.VoteResponseError("Voter exists on current voting", ErrorType.Validation);
                }

                var voting = await _votingRepository.Get(request.VotingId, cancellationToken);
                var voter = new Core.Entity.Voter(
                    voting,
                    request.FirstName,
                    request.LastName,
                    request.NationalityId);
                
                await _voterRepository.Create(voter, cancellationToken);
                _logger.LogInformation("Voter for voting with Id:{voting.id}", request.VotingId);

                return new CreateVoterResponse
                {
                    ActionResult =
                        new AppActionResult<int>
                        {
                            IsSuccesfull = true,
                            Data = voter.Id
                        }
                };
            }
            catch (EntityValidationException e)
            {
                return CreateVoterResponse.VoteResponseError(e.Message, ErrorType.Validation);
            }
        }
    }
}