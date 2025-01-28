using earfest.API.Features.Moods;
using earfest.Shared.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace earfest.API.Controllers;

public class MoodController(IMediator _mediator) : EarfestBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetMoods.Query());
        return CreateActionResult(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetMoodById.Query(id));
        return CreateActionResult(result);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMood.Command command)
    {
        var result = await _mediator.Send(command);
        return CreateActionResult(result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateMood.Command command)
    {
        var updatedCommand = command with {Id = id};
        var result = await _mediator.Send(updatedCommand);
        return CreateActionResult(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _mediator.Send(new DeleteMood.Command(id));
        return CreateActionResult(result);
    }
}
