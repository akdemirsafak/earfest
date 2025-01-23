using earPass.Domain.Models.Eventy;
using FluentValidation;

namespace earPass.Service.Validations;

public sealed class EventRequestValidator : AbstractValidator<EventRequest>
{
    public EventRequestValidator()
    {
        RuleFor(x => x.name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(1, 50).WithMessage("The name must be between 1 and 50 characters.")
            .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Name must contain only letters and numbers.");
            
            

        RuleFor(x => x.location)
            .NotEmpty().WithMessage("Location is required.")
            .Length(1, 256).WithMessage("Location must be less than 256 characters.");

        RuleFor(x => x.type)
            .NotEmpty().WithMessage("Type is required.")
            .GreaterThan(0).WithMessage("Type must be greater than 0.");
    }
}
