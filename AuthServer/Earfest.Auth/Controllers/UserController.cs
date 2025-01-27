using earfest.Shared.Base;
using Earfest.Auth.AbstractServices;
using Earfest.Auth.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Earfest.Auth.Controllers;

[Authorize]
public class UserController : EarfestBaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return CreateActionResult(await _userService.GetUsersAsync());
        //Burada tüm kullancıları döndüreceğiz.
    }

    [HttpGet("profile")]
    //[Authorize]
    public async Task<IActionResult> Profile()
    {
        return CreateActionResult(await _userService.ProfileAsync());
        //Burada kullanıcının profil bilgilerini döndüreceğiz. Jwt'den gelecek token'dan kullanıcının id'si elde edilir.
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
    {
        return CreateActionResult(await _userService.UpdateAsync(request));
        //Burada kullanıcının profil bilgilerini güncelleyelim. Jwt'den gelecek token'dan kullanıcının id'si elde edilir.
    }
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var result = await _userService.ChangePasswordAsync(request);
        return CreateActionResult(result);
    }
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var result = await _userService.LogoutAsync();
        return CreateActionResult(result);
    }
}
