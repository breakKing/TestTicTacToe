using FluentValidation;
using Gaming.Application.Players;

namespace Gaming.Application.Lobbies.Join;

internal sealed class JoinLobbyCommandValidator : AbstractValidator<JoinLobbyCommand>
{
    public JoinLobbyCommandValidator(IPlayerReadRepository playerReadRepository)
    {
        RuleFor(c => c.PlayerId)
            .SetValidator(new PlayerIdValidator(playerReadRepository));
    }
}