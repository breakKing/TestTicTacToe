using ErrorOr;
using MediatR;

namespace Gaming.Application.Common.Handling;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, ErrorOr<TResponse>>
    where TQuery : IQuery<TResponse>
{
    
}