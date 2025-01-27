using earfest.Shared.Base;
using earPass.Domain.Models.Ticket;
using earPass.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace earPass.API.Controllers;
public class TicketController(ITicketService _ticketService) : EarfestBaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(TicketRequest request)
    {
        return CreateActionResult(await _ticketService.CreateAsync(request));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        return CreateActionResult(await _ticketService.DeleteAsync(id));
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return CreateActionResult(await _ticketService.GetAllAsync());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        return CreateActionResult(await _ticketService.GetByIdAsync(id));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, TicketRequest request)
    {
        return CreateActionResult(await _ticketService.UpdateAsync(id, request));
    }
}
