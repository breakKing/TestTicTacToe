using FluentValidation;
using Gaming.Application.Players;

namespace Gaming.Application.Lobbies.Disband;

internal sealed class DisbandLobbyCommandValidator : AbstractValidator<DisbandLobbyCommand>
{
    public DisbandLobbyCommandValidator(IPlayerReadRepository playerReadRepository)
    {
        RuleFor(c => c.PlayerId)
            .SetValidator(new PlayerIdValidator(playerReadRepository));
    }
}