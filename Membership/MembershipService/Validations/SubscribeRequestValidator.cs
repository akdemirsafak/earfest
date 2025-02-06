using FluentValidation;
using MembershipService.Models.Subscriptions;

namespace MembershipService.Validations;

public class SubscribeRequestValidator : AbstractValidator<SubscribeRequest>    
{
    public SubscribeRequestValidator()
    {
        
        RuleFor(x => x.PlanId)
            .NotEmpty().WithMessage("Plan id boş bırakılamaz.");

        RuleFor(x => x.CardHolderName)
            .NotEmpty().WithMessage("Kart sahibi adı boş bırakılamaz.")
            .Length(3, 32).WithMessage("Kart sahibi adı 3-32 karakter aralığında olmalıdır.");

        RuleFor(x => x.CardNumber)
            .NotEmpty().WithMessage("Kart numarası boş bırakılamaz.")
            .Length(16).WithMessage("Kart numarası 16 karakter olmalıdır.");

        RuleFor(x => x.Cvc)
            .NotNull().WithMessage("Cvc boş bırakılamaz.")
            .InclusiveBetween(100, 999).WithMessage("Cvc 3 karakterli olmalıdır.");

        RuleFor(x => x.ExpiryMonth)
            .NotEmpty().WithMessage("Son kullanma ayı boş bırakılamaz.")
            .InclusiveBetween(1, 12).WithMessage("Son kullanma ayı 1-12 aralığında olmalıdır.");

        RuleFor(x => x.ExpiryYear)
            .NotEmpty().WithMessage("Son kullanma yılı boş bırakılamaz.")
            .InclusiveBetween(2021, 2030).WithMessage($"Son kullanma yılı {DateTime.Now.Year} - 2030 aralığında olmalıdır.");
    }
}
