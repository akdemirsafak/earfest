using Earfest.Auth.Models.User;
using FluentValidation;

namespace Earfest.Auth.Validators.User;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }
}
