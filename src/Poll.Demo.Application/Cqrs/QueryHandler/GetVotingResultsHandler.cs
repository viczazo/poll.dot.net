using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Poll.Demo.Application.Cqrs.Abstractions;
using Poll.Demo.Application.Cqrs.Query;
using Poll.Demo.Application.Model;
using Poll.Demo.Application.Model.Voting;
using Poll.Demo.Application.Repository;
using Poll.Demo.Core.Entity;

namespace Poll.Demo.Application.Cqrs.QueryHandler
{
    public class GetVotingResultsHandler : IRequestHandler<GetVotingResults, AppActionResult<VotingResults>>
    {
        private readonly IReadVotingRepository _readVotingRepository;
        private readonly ILogger<GetVotingResultsHandler> _logger;

        public GetVotingResultsHandler(IReadVotingRepository readVotingRepository, ILogger<GetVotingResultsHandler> logger)
        {
            _readVotingRepository = readVotingRepository;
            _logger = logger;
        }
        public async Task<AppActionResult<VotingResults>> Handle(GetVotingResults request, CancellationToken cancellationToken)
        {
            var results = await _readVotingRepository.GetResults(request.VotingId, cancellationToken);

            if (results != null)
                return new AppActionResult<VotingResults>
                {
                    Data = results
                };
            
            _logger.LogError("Voting with Id {voting.id} not found or not closed", request.VotingId);
            return new AppActionResult<VotingResults>
            {
                IsSuccesfull = false,
                ErrorType = ErrorType.Validation,
                ErrorMessage = "Voting not found or not closed"
            };
        }
    }
}