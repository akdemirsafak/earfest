using earfest.Shared.Base;
using MembershipService.Models.Plans;
using MembershipService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MembershipService.Controllers;

public class PlanController(IPlanService _planService) : EarfestBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        return CreateActionResult(await _planService.GetAsync());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _planService.GetByIdAsync(id);
        return CreateActionResult(result);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlanRequest request)
    {
        var result = await _planService.CreateAsync(request);
        return CreateActionResult(result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdatePlanRequest request)
    {
        return CreateActionResult(await _planService.UpdateAsync(id, request));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return CreateActionResult(await _planService.DeleteAsync(id));
    }
}
