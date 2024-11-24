using earfest.API.Base;
using earfest.API.Features.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace earfest.API.Controllers;

public class CategoryController : EarfestBaseController
{
    public CategoryController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new CategoryGetAll.Query());
        return CreateActionResult(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new CategoryGetById.Query(id));
        return CreateActionResult(result);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryCreate.Command command)
    {
        var result = await _mediator.Send(command);
        return CreateActionResult(result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] CategoryUpdate.Command command)
    {
        var updatedCommand = command with {Id = id};
        var result = await _mediator.Send(updatedCommand);
        return CreateActionResult(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _mediator.Send(new CategoryDelete.Command(id));
        return CreateActionResult(result);
    }
}
