using Earfest.Auth.Models.Auth;
using FluentValidation;

namespace Earfest.Auth.Validators.Auth;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Password)
            .NotEmpty();
    }
}