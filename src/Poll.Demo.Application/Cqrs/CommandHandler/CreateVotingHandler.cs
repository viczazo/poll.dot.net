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
    public class CreateVotingHandler : IRequestHandler<CreateVotingCommand, AppActionResult<int>>
    {
        private readonly IVotingRepository _votingRepository;
        private readonly ILogger<CreateVotingHandler> _logger;

        public CreateVotingHandler(IVotingRepository votingRepository,
            ILogger<CreateVotingHandler> logger)
        {
            _votingRepository = votingRepository;
            _logger = logger;
        }

        public async Task<AppActionResult<int>> Handle(CreateVotingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var voting = new Core.Entity.Voting(request.Name);
                await _votingRepository.Create(voting, cancellationToken);
                _logger.LogInformation("Voting with name: {voting.name} created with Id:{voting.id}", voting.Name,
                    voting.Id);

                return new AppActionResult<int>
                {
                    IsSuccesfull = true,
                    Data = voting.Id
                };
            }
            catch (EntityValidationException e)
            {
                return new AppActionResult<int>
                {
                    ErrorMessage = e.Message,
                    ErrorType = ErrorType.Validation,
                    Data = default
                };
            }
        }
    }
}
