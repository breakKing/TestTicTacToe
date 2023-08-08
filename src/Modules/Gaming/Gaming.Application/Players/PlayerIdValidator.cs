using FluentValidation;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Application.Players;

internal sealed class PlayerIdValidator : AbstractValidator<Guid>
{
    private readonly IPlayerReadRepository _readRepository;
    
    public PlayerIdValidator(IPlayerReadRepository readRepository)
    {
        _readRepository = readRepository;
        
        RuleFor(guid => guid)
            .NotEmpty()
            .WithMessage("Идентификатор игрока не может быть пустым");
        
        RuleFor(guid => guid)
            .MustAsync(PlayerExistsAsync)
            .WithMessage("Указанный игрок не существует")
            .When(guid => guid != default);
    }
    
    private async Task<bool> PlayerExistsAsync(
        Guid playerGuid,
        CancellationToken ct = default)
    {
        var playerId = PlayerId.CreateFromGuid(playerGuid);

        return await _readRepository.GetByIdAsync(playerId, ct) is not null;
    }
}