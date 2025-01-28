using earfest.API.Features.Orders;
using earfest.Shared.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace earfest.API.Controllers;

public class OrderController(IMediator _mediator) : EarfestBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return CreateActionResult(await _mediator.Send(new GetOrders.Query()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return CreateActionResult(await _mediator.Send(new GetOrderById.Query(id)));
    }
}
