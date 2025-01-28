using earfest.API.Domain.DbContexts;
using earfest.API.Models.Plans;
using earfest.Shared.Base;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace earfest.API.Features.Plans;

public static class GetPlans
{
    public record Query : IRequest<AppResult<List<PlanResponse>>>;

    public class QueryHandler(EarfestDbContext _context) : IRequestHandler<Query, AppResult<List<PlanResponse>>>
    {
        public async Task<AppResult<List<PlanResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var plans = await _context.Plans.ToListAsync();
            var response = plans.Adapt<List<PlanResponse>>();
            return AppResult<List<PlanResponse>>.Success(response,200);
        }
    }
}
