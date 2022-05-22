using Microsoft.Extensions.DependencyInjection;
using Poll.Demo.Application.Repository;
using Poll.Demo.Infrastructure.Persistence.RepositoryEf;
using Poll.Demo.Infrastructure.Persistence.RepositoryEf.Configuration;

namespace Poll.Demo.Infrastructure.DI;

public static class DbExtensions
{
    public static void AddRelationalDbOrm(this IServiceCollection services)
    {
        services.AddDbContext<PollDbContext>();
        services.AddScoped<IVotingRepository, VotingRepository>();
        services.AddScoped<IVoterRepository, VoterRepository>();
        services.AddScoped<IReadVotingRepository, ReadVotingRepository>();
    }
}