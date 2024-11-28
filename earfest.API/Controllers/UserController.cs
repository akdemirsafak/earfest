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
        return Ok();
        //Burada kullanıcının profil bilgilerini döndüreceğiz. Jwt'den gelecek token'dan kullanıcının id'si elde edilir.
    }
    [HttpPut]
    public async Task<IActionResult> Update()
    {
        return Ok();
        //Burada kullanıcının profil bilgilerini güncelley
    }
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePassword.Command command)
    {
        var result = await _mediator.Send(command);
        return CreateActionResult(result);
    }

}
