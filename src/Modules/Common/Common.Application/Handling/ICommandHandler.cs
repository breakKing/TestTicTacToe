using ErrorOr;
using MediatR;

namespace Common.Application.Handling;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, ErrorOr<TResponse>>
    where TCommand : ICommand<TResponse>
{
    
}

public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, bool>
    where TCommand : ICommand
{
    
}