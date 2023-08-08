using FluentValidation;
using Gaming.Application.Players;

namespace Gaming.Application.Lobbies.Create;

internal sealed class CreateLobbyCommandValidator : AbstractValidator<CreateLobbyCommand>
{
    public CreateLobbyCommandValidator(IPlayerReadRepository playerReadRepository)
    {
        RuleFor(c => c.PlayerId)
            .SetValidator(new PlayerIdValidator(playerReadRepository));
    }
}