using FastEndpoints;
using FluentValidation;

namespace Identity.Core.Features.Register;

public sealed class RegisterValidator : Validator<RegisterRequest>
{
    private const int MinimalPasswordLength = 5;
    
    public RegisterValidator()
    {
        RuleFor(r => r.Username)
            .NotEmpty()
            .WithMessage("Имя пользователя должно быть задано");
        
        RuleFor(r => r.Password)
            .MinimumLength(MinimalPasswordLength)
            .WithMessage($"Пароль должен иметь как минимум {MinimalPasswordLength} символов");
    }
}