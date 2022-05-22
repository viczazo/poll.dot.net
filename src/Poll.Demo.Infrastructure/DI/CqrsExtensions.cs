using Microsoft.Extensions.DependencyInjection;
using Poll.Demo.Application.Cqrs.Abstractions;
using Poll.Demo.Application.Cqrs.Command;
using Poll.Demo.Application.Cqrs.CommandHandler;
using Poll.Demo.Application.Cqrs.Query;
using Poll.Demo.Application.Cqrs.QueryHandler;
using Poll.Demo.Application.Model;
using Poll.Demo.Application.Model.Voting;

namespace Poll.Demo.Infrastructure.DI;

public static class CqrsExtensions
{
    public static void AddCqrs(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CreateVoteCommand, CreateVoteResponse>, CreateVoteHandler>();
        services.AddScoped<IRequestHandler<CreateVoterCommand, CreateVoterResponse>, CreateVoterHandler>();
        services.AddScoped<IRequestHandler<ChangeVotingStateCommand, AppActionResult>, ChangeVotingStateHandler>();
        services.AddScoped<IRequestHandler<CreateVotingCommand, AppActionResult<int>>, CreateVotingHandler>();
        services.AddScoped<IRequestHandler<GetVotingResults, AppActionResult<VotingResults>>, GetVotingResultsHandler>();
    }
}