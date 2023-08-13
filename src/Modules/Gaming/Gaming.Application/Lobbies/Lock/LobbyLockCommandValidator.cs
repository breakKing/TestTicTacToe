using FluentValidation;
using Gaming.Application.Players;

namespace Gaming.Application.Lobbies.Lock;

internal sealed class LobbyLockCommandValidator : AbstractValidator<LobbyLockCommand>
{
    public LobbyLockCommandValidator(IPlayerReadRepository playerReadRepository)
    {
        RuleFor(c => c.PlayerId)
            .SetValidator(new PlayerIdValidator(playerReadRepository));
    }
}