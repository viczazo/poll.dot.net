using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Poll.Demo.Application.Cqrs.Command;
using Poll.Demo.Application.Model;
using Poll.Demo.Infrastructure.Queue;

namespace Poll.Demo.Infrastructure.DI;

public static class QueueExtensions
{
    public static void AddQueueManager(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<ExternalHandlerProxy<CreateVoteCommand, CreateVoteResponse>>();
            x.AddConsumer<ExternalHandlerProxy<CreateVoterCommand, CreateVoterResponse>>();
            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
            // x.UsingRabbitMq((context, cfg) =>
            // {
            //     cfg.Host("localhost", "/", h =>
            //     {
            //         h.Username("guest");
            //         h.Password("guest");
            //     });
            //     cfg.ConfigureEndpoints(context);
            // });
        });
    }
}