using System.Transactions;
using Gaming.Application.Common.Handling;
using Gaming.Application.Common.Persistence;
using Gaming.Domain.Games.Entities;
using Gaming.Domain.Lobbies.DomainEvents;

namespace Gaming.Application.Games.Start;

internal sealed class CreateGameAfterLobbyLockedHandler : IDomainEventHandler<LobbyLockedForGameStartDomainEvent>
{
    private readonly IGameWriteRepository _writeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGameAfterLobbyLockedHandler(IGameWriteRepository writeRepository, IUnitOfWork unitOfWork)
    {
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task Handle(LobbyLockedForGameStartDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        var game = new Game(domainEvent.FirstPlayerId, domainEvent.SecondPlayerId, domainEvent.LobbyId);

        _writeRepository.Add(game);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        transactionScope.Complete();
    }
}