using earPass.Domain.Models.Ticket;
using FluentValidation;

namespace earPass.Service.Validations;

public sealed class TicketRequestValidator : AbstractValidator<TicketRequest>
{
    public TicketRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(1, 50).WithMessage("Name must be less than 50 characters."); 
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required")
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than 0 or equal 0.");
        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("Quantity is required.")
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        RuleFor(x => x.EventId)
            .NotEmpty().WithMessage("EventId is required.");
    }
}
