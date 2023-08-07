using ErrorOr;
using MediatR;

namespace Common.Application.Handling;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, ErrorOr<TResponse>>
    where TQuery : IQuery<TResponse>
{
    
}