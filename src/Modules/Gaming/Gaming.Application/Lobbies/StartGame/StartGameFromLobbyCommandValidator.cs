using FluentValidation;
using Gaming.Application.Players;

namespace Gaming.Application.Lobbies.StartGame;

internal sealed class StartGameFromLobbyCommandValidator : AbstractValidator<StartGameFromLobbyCommand>
{
    public StartGameFromLobbyCommandValidator(
        IPlayerReadRepository playerReadRepository,
        ILobbyReadRepository lobbyReadRepository)
    {
        RuleFor(c => c.PlayerId)
            .SetValidator(new PlayerIdValidator(playerReadRepository));
        
        RuleFor(c => c.LobbyId)
            .SetValidator(new LobbyIdValidator(lobbyReadRepository));
    }
}