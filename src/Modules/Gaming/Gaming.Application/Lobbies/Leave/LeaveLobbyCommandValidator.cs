using FluentValidation;
using Gaming.Application.Players;

namespace Gaming.Application.Lobbies.Leave;

internal sealed class LeaveLobbyCommandValidator : AbstractValidator<LeaveLobbyCommand>
{
    public LeaveLobbyCommandValidator(IPlayerReadRepository playerReadRepository)
    {
        RuleFor(c => c.PlayerId)
            .SetValidator(new PlayerIdValidator(playerReadRepository));
    }
}