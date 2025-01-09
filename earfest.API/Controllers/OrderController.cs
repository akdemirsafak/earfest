using earfest.API.Base;
using earfest.API.Features.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace earfest.API.Controllers;

public class OrderController : EarfestBaseController
{
    public OrderController(IMediator mediator) : base(mediator)
    {
    }

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
