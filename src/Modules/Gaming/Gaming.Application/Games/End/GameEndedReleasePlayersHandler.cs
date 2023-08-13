using System.Transactions;
using Gaming.Application.Common.Handling;
using Gaming.Application.Common.Persistence;
using Gaming.Application.Lobbies;
using Gaming.Domain.Games.DomainEvents;

namespace Gaming.Application.Games.End;

internal sealed class GameEndedReleasePlayersHandler : IDomainEventHandler<GameEndedDomainEvent>
{
    private readonly ILobbyReadRepository _lobbyReadRepository;
    private readonly ILobbyWriteRepository _lobbyWriteRepository;

    public GameEndedReleasePlayersHandler(
        ILobbyReadRepository lobbyReadRepository,
        ILobbyWriteRepository lobbyWriteRepository)
    {
        _lobbyReadRepository = lobbyReadRepository;
        _lobbyWriteRepository = lobbyWriteRepository;
    }

    /// <inheritdoc />
    public async Task Handle(GameEndedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var lobbyId = await _lobbyReadRepository.GetLobbyIdByGameIdAsync(domainEvent.GameId, cancellationToken);

        if (lobbyId is null)
        {
            return;
        }
        
        var lobby = await _lobbyWriteRepository.LoadAsync(lobbyId, cancellationToken);

        if (lobby is null)
        {
            return;
        }

        _lobbyWriteRepository.Delete(lobby);
    }
}