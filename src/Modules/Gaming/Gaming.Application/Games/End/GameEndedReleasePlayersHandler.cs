using System.Transactions;
using Gaming.Application.Common.Handling;
using Gaming.Application.Common.Persistence;
using Gaming.Application.Lobbies;
using Gaming.Domain.Games.DomainEvents;

namespace Gaming.Application.Games.End;

internal sealed class GameEndedReleasePlayersHandler : IDomainEventHandler<GameEndedDomainEvent>
{
    private readonly IGameWriteRepository _gameWriteRepository;
    private readonly ILobbyWriteRepository _lobbyWriteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GameEndedReleasePlayersHandler(
        IGameWriteRepository gameWriteRepository,
        ILobbyWriteRepository lobbyWriteRepository, 
        IUnitOfWork unitOfWork)
    {
        _gameWriteRepository = gameWriteRepository;
        _lobbyWriteRepository = lobbyWriteRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task Handle(GameEndedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        var game = await _gameWriteRepository.LoadAsync(domainEvent.GameId, cancellationToken);

        if (game is null)
        {
            return;
        }

        var lobby = await _lobbyWriteRepository.LoadAsync(game.StartedFromLobbyId, cancellationToken);

        if (lobby is null)
        {
            return;
        }

        _lobbyWriteRepository.Delete(lobby);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        transactionScope.Complete();
    }
}