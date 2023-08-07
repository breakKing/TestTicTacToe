using ErrorOr;
using MediatR;

namespace Common.Application.Handling;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, ErrorOr<bool>>
    where TCommand : ICommand
{
    
}