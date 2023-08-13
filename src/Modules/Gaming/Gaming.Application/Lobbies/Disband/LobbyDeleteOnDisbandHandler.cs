using System.Transactions;
using Gaming.Application.Common.Handling;
using Gaming.Application.Common.Persistence;
using Gaming.Domain.Lobbies.DomainEvents;

namespace Gaming.Application.Lobbies.Disband;

internal sealed class LobbyDeleteOnDisbandHandler : IDomainEventHandler<LobbyDisbandedDomainEvent>
{
    private readonly ILobbyWriteRepository _writeRepository;

    public LobbyDeleteOnDisbandHandler(ILobbyWriteRepository writeRepository)
    {
        _writeRepository = writeRepository;
    }

    /// <inheritdoc />
    public async Task Handle(LobbyDisbandedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var lobby = await _writeRepository.LoadAsync(domainEvent.LobbyId, cancellationToken);

        if (lobby is null)
        {
            return;
        }
        
        _writeRepository.Delete(lobby);
    }
}