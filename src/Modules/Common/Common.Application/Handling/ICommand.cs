using ErrorOr;
using MediatR;

namespace Common.Application.Handling;

public interface ICommand : IRequest<ErrorOr<bool>>
{
    
}