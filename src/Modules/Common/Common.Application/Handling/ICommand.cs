using ErrorOr;
using MediatR;

namespace Common.Application.Handling;

public interface ICommand<TResponse> : IRequest<ErrorOr<TResponse>>
{
    
}

public interface ICommand : ICommand<bool>
{
    
}