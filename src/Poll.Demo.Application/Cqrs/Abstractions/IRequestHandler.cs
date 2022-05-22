using System.Threading;
using System.Threading.Tasks;

namespace Poll.Demo.Application.Cqrs.Abstractions
{
    public interface IRequestHandler { }
    public interface IRequestHandler<in TRequest, TResponse> : IRequestHandler
        where TRequest : IRequest
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}