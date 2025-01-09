using earfest.API.Base;
using earfest.API.Features.Contents;
using earfest.API.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace earfest.API.Controllers;

public class ContentController : EarfestBaseController
{
    public ContentController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetContents.Query());
        return CreateActionResult(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetContentById.Query(id));
        return CreateActionResult(result);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContent.Command command)
    {
        var result = await _mediator.Send(command);
        return CreateActionResult(result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateContent.Command command)
    {
        var updatedCommand = command with {Id = id};
        var result = await _mediator.Send(updatedCommand);
        return CreateActionResult(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _mediator.Send(new DeleteContent.Command(id));
        return CreateActionResult(result);
    }

    //[Authorize]
    [MembershipRequirement("Premium")]
    [HttpGet("PremiumContents")]
    public IActionResult GetPremiumContent()
    {
        //var membershipType = HttpContext.User.FindFirst("MembershipType")?.Value;

        //if (membershipType == "Premium")
        //{
        //    return Ok(new { Content = "Premium Content" });
        //}

        //return Ok(new { Content = "Standard Content" });
        // Premium içerik döndür
        return Ok("This is premium content");
    }
}
