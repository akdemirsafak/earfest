using earfest.API.Base;
using earfest.API.Features.Membership;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace earfest.API.Controllers;

[Authorize]
public class MembershipController : EarfestBaseController
{
    public MembershipController(IMediator mediator) : base(mediator)
    {
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
