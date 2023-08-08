using FluentValidation;
using Gaming.Application.Players;

namespace Gaming.Application.Lobbies.GetAvailable;

internal sealed class GetAvailableLobbiesQueryValidator : AbstractValidator<GetAvailableLobbiesQuery>
{
    public GetAvailableLobbiesQueryValidator(IPlayerReadRepository playerReadRepository)
    {
        RuleFor(c => c.PlayerId)
            .SetValidator(new PlayerIdValidator(playerReadRepository));
    }
}