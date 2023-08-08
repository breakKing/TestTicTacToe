using FluentValidation;
using Gaming.Application.Players;

namespace Gaming.Application.Lobbies.GetCurrent;

internal sealed class GetPlayerCurrentLobbyQueryValidator : AbstractValidator<GetPlayerCurrentLobbyQuery>
{
    public GetPlayerCurrentLobbyQueryValidator(IPlayerReadRepository playerReadRepository)
    {
        RuleFor(c => c.PlayerId)
            .SetValidator(new PlayerIdValidator(playerReadRepository));
    }
}