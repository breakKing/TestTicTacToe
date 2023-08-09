using FluentValidation;
using Gaming.Application.Players;

namespace Gaming.Application.Lobbies.StartGame;

internal sealed class StartGameFromLobbyCommandValidator : AbstractValidator<StartGameFromLobbyCommand>
{
    public StartGameFromLobbyCommandValidator(IPlayerReadRepository playerReadRepository)
    {
        RuleFor(c => c.PlayerId)
            .SetValidator(new PlayerIdValidator(playerReadRepository));
    }
}