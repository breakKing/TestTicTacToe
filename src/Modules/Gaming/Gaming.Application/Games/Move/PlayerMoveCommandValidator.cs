using FluentValidation;
using Gaming.Application.Players;

namespace Gaming.Application.Games.Move;

internal sealed class PlayerMoveCommandValidator : AbstractValidator<PlayerMoveCommand>
{
    public PlayerMoveCommandValidator(IPlayerReadRepository playerReadRepository)
    {
        RuleFor(c => c.PlayerId)
            .SetValidator(new PlayerIdValidator(playerReadRepository));
    }
}