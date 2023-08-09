using FluentValidation;
using Gaming.Domain.Lobbies.ValueObjects;

namespace Gaming.Application.Lobbies;

internal sealed class LobbyIdValidator : AbstractValidator<Guid>
{
    private readonly ILobbyReadRepository _readRepository;
    
    public LobbyIdValidator(ILobbyReadRepository readRepository)
    {
        _readRepository = readRepository;
        
        RuleFor(guid => guid)
            .NotEmpty()
            .WithMessage("Идентификатор лобби не может быть пустым");
        
        RuleFor(guid => guid)
            .MustAsync(LobbyExistsAsync)
            .WithMessage("Указанное лобби не существует")
            .When(guid => guid != Guid.Empty);
    }
    
    private async Task<bool> LobbyExistsAsync(
        Guid lobbyGuid,
        CancellationToken ct = default)
    {
        var lobbyId = LobbyId.CreateFromGuid(lobbyGuid);

        return await _readRepository.GetByIdAsync(lobbyId, ct) is not null;
    }
}