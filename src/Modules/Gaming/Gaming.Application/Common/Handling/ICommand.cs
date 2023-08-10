using ErrorOr;
using MediatR;

namespace Gaming.Application.Common.Handling;

public interface ICommand<TResponse> : IRequest<ErrorOr<TResponse>>
{
    
}

public interface ICommand : ICommand<bool>
{
    
}