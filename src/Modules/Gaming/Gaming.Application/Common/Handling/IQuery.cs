using ErrorOr;
using MediatR;

namespace Gaming.Application.Common.Handling;

public interface IQuery<TResponse> : IRequest<ErrorOr<TResponse>>
{
    
}