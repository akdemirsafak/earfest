using earfest.API.Features.Membership;
using earfest.Shared.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace earfest.API.Controllers;

[Authorize]
public class MembershipController : EarfestBaseController
{
    private readonly IMediator _mediator;

    public MembershipController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe([FromBody] Subscribe.Command command)
    {
        var result = await _mediator.Send(command);
        return CreateActionResult(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Unsubscribe(string id)
    {
        var result = await _mediator.Send(new Unsubscribe.Command(id));
        return CreateActionResult(result);
    }
}
