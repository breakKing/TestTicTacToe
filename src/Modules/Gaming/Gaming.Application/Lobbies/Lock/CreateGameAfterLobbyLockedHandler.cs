using System.Transactions;
using Gaming.Application.Common.Handling;
using Gaming.Application.Common.Persistence;
using Gaming.Application.Games;
using Gaming.Domain.Games.Entities;
using Gaming.Domain.Lobbies.DomainEvents;

namespace Gaming.Application.Lobbies.Lock;

internal sealed class CreateGameAfterLobbyLockedHandler : IDomainEventHandler<LobbyLockedForGameStartDomainEvent>
{
    private readonly IGameWriteRepository _gameRepository;
    private readonly ILobbyWriteRepository _lobbyRepository;

    public CreateGameAfterLobbyLockedHandler(
        IGameWriteRepository gameRepository, 
        ILobbyWriteRepository lobbyRepository) 
    {
        _gameRepository = gameRepository;
        _lobbyRepository = lobbyRepository;
    }

    /// <inheritdoc />
    public async Task Handle(LobbyLockedForGameStartDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var lobby = await _lobbyRepository.LoadAsync(domainEvent.LobbyId, cancellationToken);

        if (lobby is null)
        {
            return;
        }
        
        var game = new Game(domainEvent.FirstPlayerId, domainEvent.SecondPlayerId, domainEvent.LobbyId);
        _gameRepository.Add(game);
        
        lobby.SetGame(game);
        _lobbyRepository.Update(lobby);
    }
}