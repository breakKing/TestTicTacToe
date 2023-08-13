using ErrorOr;
using Gaming.Application.Common.Handling;
using Gaming.Domain.Players.Entities;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Application.Players.Create;

internal sealed class PlayerCreateCommandHandler : ICommandHandler<PlayerCreateCommand>
{
    private const string PlayerAlreadyExistsErrorDescription = "Данный игрок уже существует";
    
    private readonly IPlayerReadRepository _readRepository;
    private readonly IPlayerWriteRepository _writeRepository;

    public PlayerCreateCommandHandler(IPlayerReadRepository readRepository, IPlayerWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    /// <inheritdoc />
    public async Task<ErrorOr<bool>> Handle(PlayerCreateCommand request, CancellationToken cancellationToken)
    {
        var playerId = PlayerId.CreateFromGuid(request.Id);

        if (await _readRepository.ExistsAsync(playerId, cancellationToken))
        {
            return Error.Validation(description: PlayerAlreadyExistsErrorDescription);
        }

        var player = new Player(playerId, request.Username);
        
        _writeRepository.Add(player);

        return true;
    }
}