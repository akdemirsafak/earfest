using earfest.API.Base;
using earfest.API.Features.Auths;
using earfest.API.Models.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace earfest.API.Controllers;

public class AuthController : EarfestBaseController
{
    public AuthController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Register.Command command)
    {
        var result = await _mediator.Send(command);
        return CreateActionResult(result);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login.Query query)
    {
        var result = await _mediator.Send(query);
        return CreateActionResult(result);
    }
    //[HttpPost("refresh")]
    //public async Task<IActionResult> Refresh([FromBody] Refresh.Query query)
    //{
    //    var result = await _mediator.Send(query);
    //    return CreateActionResult(result);
    //}

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword.Command command)
    {
        var result = await _mediator.Send(command);
        return CreateActionResult(result);
    }
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request, string email, string token)
    {

        var result = await _mediator.Send(new ResetPassword.Command(email, token, request.Password));
        return CreateActionResult(result);
    }



    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmail.Command command)
    {
        var result = await _mediator.Send(command);
        return CreateActionResult(result);
    }
}
