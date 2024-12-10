using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Models.Plans;
using FluentValidation;
using Mapster;
using MediatR;

namespace earfest.API.Features.Plans;

public static class GetPlanById
{
    public record Query(string Id) : IRequest<AppResult<PlanResponse>>;
    public class QueryHandler(EarfestDbContext _context) : IRequestHandler<Query, AppResult<PlanResponse>>
    {
        public async Task<AppResult<PlanResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var mood = await _context.Plans.FindAsync(request.Id);
            return AppResult<PlanResponse>.Success(mood.Adapt<PlanResponse>(), 200);
        }
    }

    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}
