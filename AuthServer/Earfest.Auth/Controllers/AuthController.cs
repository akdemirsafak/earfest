using earfest.Shared.Base;
using Earfest.Auth.AbstractServices;
using Earfest.Auth.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Earfest.Auth.Controllers;

public class AuthController : EarfestBaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        return CreateActionResult(await _authService.RegisterAsync(request));
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);
        return CreateActionResult(result);
    }
    //[HttpPost("refresh")]
    //public async Task<IActionResult> Refresh([FromBody] Refresh.Query query)
    //{
    //    var result = await _mediator.Send(query);
    //    return CreateActionResult(result);
    //}

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        var result = await _authService.ForgotPasswordAsync(request);
        return CreateActionResult(result);
    }
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromQuery] string userId, [FromQuery] string token, [FromBody] ResetPasswordRequest request)
    {

        var result = await _authService.ResetPasswordAsync(userId,token, request.Password);
        return CreateActionResult(result);
    }


    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequest request)
    {
        var result = await _authService.ConfirmEmailAsync(request);
        return CreateActionResult(result);
    }
}
