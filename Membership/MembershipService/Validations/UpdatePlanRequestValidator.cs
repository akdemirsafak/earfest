using FluentValidation;
using MembershipService.Models.Plans;

namespace MembershipService.Validations;

public class UpdatePlanRequestValidator : AbstractValidator<UpdatePlanRequest>
{
    public UpdatePlanRequestValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Plan adı boş bırakılamaz.")
           .Length(3, 32).WithMessage("Plan adı 3-32 karakter aralığında olmalıdır.");

        RuleFor(x => x.Description)
            .MaximumLength(255).WithMessage("Açıklama maksimum 255 karakter olabilir.");

        RuleFor(x => x.Duration)
            .GreaterThanOrEqualTo(0).WithMessage("Üyelik planı süresi 0 dan büyük olmalıdır.");

        RuleFor(x => x.IsFree)
            .Equal(false).When(x => x.IsPremium)
            .WithMessage("Premium plan ücretsiz olamaz.");

        RuleFor(x => x.IsTrial)
            .Equal(false).When(x => x.Price != 0)
            .WithMessage("Deneme üyeliği ücretli olamaz.");

        RuleFor(x => x.IsFree)
            .Must((model, isFree) => !isFree || model.Price == 0)
            .WithMessage("Ücretsiz planın fiyatı 0 olmalıdır.");
    }
}
