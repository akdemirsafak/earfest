using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Domain.Entities;
using earfest.API.Models.Plans;
using FluentValidation;
using Mapster;
using MediatR;

namespace earfest.API.Features.Plans;

public static class CreatePlan
{
    public record Command(string Name, string Description, decimal Price, int Duration, bool isTrial, bool isFree, bool isPremium) : IRequest<AppResult<PlanResponse>>;
    public class CommandHandler(EarfestDbContext _context) : IRequestHandler<Command, AppResult<PlanResponse>>
    {
        public async Task<AppResult<PlanResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            isEqualFreeAndPremium(request.isFree, request.isPremium);
            isEqualFreeAndTrial(request.isFree, request.isTrial);

            var plan = request.Adapt<Plan>();
            await _context.Plans.AddAsync(plan);
            await _context.SaveChangesAsync(cancellationToken);
            return AppResult<PlanResponse>.Success(plan.Adapt<PlanResponse>(), 201);
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(32);
            RuleFor(x => x.Description)
                .MaximumLength(256);
            RuleFor(x => x.Price)
                .NotNull()
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.Duration)
                .NotNull()
                .GreaterThanOrEqualTo(1);
        }
    }
    private static void isEqualFreeAndPremium(bool isFree, bool isPremium)
    {
        if (isFree && isPremium)
            throw new Exception("Plan cannot be both free and premium.");
    }
    private static void isEqualFreeAndTrial(bool isFree, bool isTrial)
    {
        if (isFree && isTrial)
            throw new Exception("Plan cannot be both free and trial.");
    }
}
