using earfest.Shared.Base;
using Mapster;
using MembershipService.DbContexts;
using MembershipService.Entities;
using MembershipService.Models.Plans;
using Microsoft.EntityFrameworkCore;

namespace MembershipService.Services;

public class PlanService : IPlanService
{
    private readonly MembershipDbContext _dbContext;

    public PlanService(MembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResult<PlanResponse>> CreateAsync(CreatePlanRequest request)
    {
        isEqualFreeAndPremium(request.IsFree, request.IsPremium);
        isEqualFreeAndTrial(request.IsFree, request.IsTrial);

        var plan = request.Adapt<Plan>();
        await _dbContext.Plans.AddAsync(plan);
        await _dbContext.SaveChangesAsync();
        return AppResult<PlanResponse>.Success(plan.Adapt<PlanResponse>(), 201);
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

    public async Task<AppResult<NoContentDto>> DeleteAsync(string id)
    {
        var plan = await _dbContext.Plans.FindAsync(id);
        _dbContext.Plans.Remove(plan);
        await _dbContext.SaveChangesAsync();
        return AppResult<NoContentDto>.Success(204);
    }

    public async Task<AppResult<List<PlanResponse>>> GetAsync()
    {
        var plans = await _dbContext.Plans.ToListAsync();
        var response = plans.Adapt<List<PlanResponse>>();
        return AppResult<List<PlanResponse>>.Success(response, 200);
    }

    public async Task<AppResult<PlanResponse>> GetByIdAsync(string id)
    {
        var mood = await _dbContext.Plans.FindAsync(id);
        return AppResult<PlanResponse>.Success(mood.Adapt<PlanResponse>(), 200);
    }

    public async Task<AppResult<PlanResponse>> UpdateAsync(string id, UpdatePlanRequest request)
    {
        var plan = await _dbContext.Plans.FindAsync(id);
        plan.Name = request.Name;
        plan.Description = request.Description;
        plan.Price = request.Price;
        plan.Duration = request.Duration;
        plan.IsTrial = request.IsTrial;
        plan.IsFree = request.IsFree;
        plan.IsPremium = request.IsPremium;

        await _dbContext.SaveChangesAsync();
        return AppResult<PlanResponse>.Success(plan.Adapt<PlanResponse>(), 200);
    }
}
