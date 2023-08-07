using ErrorOr;
using MediatR;

namespace Common.Application.Handling;

public interface IQuery<TResponse> : IRequest<ErrorOr<TResponse>>
{
    
}