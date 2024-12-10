using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Models.Plans;
using FluentValidation;
using Mapster;
using MediatR;

namespace earfest.API.Features.Plans;

public static class UpdatePlan
{
    public record Command(string Id, UpdatePlanRequest Model) : IRequest<AppResult<PlanResponse>>;
    public class CommandHandler(EarfestDbContext _context) : IRequestHandler<Command, AppResult<PlanResponse>>
    {
        public async Task<AppResult<PlanResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var plan = await _context.Plans.FindAsync(request.Id);
            plan.Name = request.Model.Name;
            plan.Description = request.Model.Description;
            plan.Price = request.Model.Price;
            plan.Duration = request.Model.Duration;
            plan.IsTrial = request.Model.IsTrial;
            plan.IsFree = request.Model.IsFree;
            plan.IsPremium = request.Model.IsPremium;

            await _context.SaveChangesAsync(cancellationToken);
            return AppResult<PlanResponse>.Success(plan.Adapt<PlanResponse>(), 200);
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Model.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(32);
            RuleFor(x => x.Model.Description)
                .MaximumLength(256);
            RuleFor(x => x.Model.Price)
                .NotNull()
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.Model.Duration)
                .NotNull()
                .GreaterThanOrEqualTo(1);
        }
    }
}
