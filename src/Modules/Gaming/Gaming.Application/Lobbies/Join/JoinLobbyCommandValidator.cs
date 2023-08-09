using FluentValidation;
using Gaming.Application.Players;

namespace Gaming.Application.Lobbies.Join;

internal sealed class JoinLobbyCommandValidator : AbstractValidator<JoinLobbyCommand>
{
    public JoinLobbyCommandValidator(
        IPlayerReadRepository playerReadRepository,
        ILobbyReadRepository lobbyReadRepository)
    {
        RuleFor(c => c.PlayerId)
            .SetValidator(new PlayerIdValidator(playerReadRepository));
        
        RuleFor(c => c.LobbyId)
            .SetValidator(new LobbyIdValidator(lobbyReadRepository));
    }
}