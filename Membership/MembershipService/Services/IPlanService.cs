using earfest.Shared.Base;
using MembershipService.Models.Plans;

namespace MembershipService.Services;

public interface IPlanService
{
    Task<AppResult<PlanResponse>> CreateAsync(CreatePlanRequest request);
    Task<AppResult<NoContentDto>> DeleteAsync(string id);
    Task<AppResult<PlanResponse>> GetByIdAsync(string id);
    Task<AppResult<List<PlanResponse>>> GetAsync();
    Task<AppResult<PlanResponse>> UpdateAsync(string id, UpdatePlanRequest request);
}
