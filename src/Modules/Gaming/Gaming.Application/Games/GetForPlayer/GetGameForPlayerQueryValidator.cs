using FluentValidation;
using Gaming.Application.Players;

namespace Gaming.Application.Games.GetForPlayer;

internal sealed class GetGameForPlayerQueryValidator : AbstractValidator<GetGameForPlayerQuery>
{
    public GetGameForPlayerQueryValidator(IPlayerReadRepository playerReadRepository)
    {
        RuleFor(c => c.PlayerId)
            .SetValidator(new PlayerIdValidator(playerReadRepository));
    }
}