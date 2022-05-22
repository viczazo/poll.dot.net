using System.Threading.Tasks;
using MassTransit;
using Poll.Demo.Application.Cqrs.Abstractions;

namespace Poll.Demo.Infrastructure.Queue;

public class ExternalHandlerProxy<TRequest, TResponse> : IConsumer<TRequest> 
    where TRequest : class, IRequest
    where TResponse : class
{
    private readonly IRequestHandler<TRequest, TResponse> _handler;
    
    public ExternalHandlerProxy(IRequestHandler<TRequest,TResponse> handler)
    {
        _handler = handler;
    }
    
    public async Task Consume(ConsumeContext<TRequest> context)
    {
        var response = await _handler.Handle(context.Message, context.CancellationToken);
        await context.RespondAsync(response);
    }
}