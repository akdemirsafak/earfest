using earfest.API.Base;
using earfest.API.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace earfest.API.Controllers;


public class UserController : EarfestBaseController
{
    public UserController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        return CreateActionResult(await _mediator.Send(new UserProfile.Query()));
        //Burada kullanıcının profil bilgilerini döndüreceğiz. Jwt'den gelecek token'dan kullanıcının id'si elde edilir.
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUser.Command request)
    {
        return CreateActionResult(await _mediator.Send(request));
        //Burada kullanıcının profil bilgilerini güncelleyelim. Jwt'den gelecek token'dan kullanıcının id'si elde edilir.
    }
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePassword.Command command)
    {
        var result = await _mediator.Send(command);
        return CreateActionResult(result);
    }
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] Logout.Command command)
    {
        var result = await _mediator.Send(command);
        return CreateActionResult(result);
    }

}
