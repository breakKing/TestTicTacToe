using ErrorOr;
using MediatR;

namespace Gaming.Application.Common.Handling;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, ErrorOr<TResponse>>
    where TCommand : ICommand<TResponse>
{
    
}

public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, bool>
    where TCommand : ICommand
{
    
}