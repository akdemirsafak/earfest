using earfest.Shared.Base;
using earPass.Domain.Models.Eventy;
using earPass.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace earPass.API.Controllers;

public class EventController(IEventyService _eventService) : EarfestBaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(EventRequest request)
    {
        return CreateActionResult(await _eventService.CreateAsync(request));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        return CreateActionResult(await _eventService.DeleteAsync(id));
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return CreateActionResult(await _eventService.GetAllAsync());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        return CreateActionResult(await _eventService.GetByIdAsync(id));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, EventRequest request)
    {
        return CreateActionResult(await _eventService.UpdateAsync(id, request));
    }
}
