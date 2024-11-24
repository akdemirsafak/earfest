using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace earfest.API.Base;

[Route("api/[controller]")]
[ApiController]
public class EarfestBaseController : ControllerBase
{
    protected readonly IMediator _mediator;

    public EarfestBaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [NonAction]
    public IActionResult CreateActionResult<T>(AppResult<T> response)
    {
        if (response.StatusCode == 204)
            return new ObjectResult(null) { StatusCode = response.StatusCode };
        return new ObjectResult(response) { StatusCode = response.StatusCode };
    }
}
