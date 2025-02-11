using FluentValidation;
using NotificationAPI.Models;

namespace NotificationAPI.Validations;

public class SendEmailRequestValidator : AbstractValidator<SendEmailRequest>
{
    public SendEmailRequestValidator()
    {
        RuleFor(x => x.To).NotEmpty().EmailAddress();
        RuleFor(x => x.Subject).NotEmpty();
        RuleFor(x => x.Body).NotEmpty();
    }
}
