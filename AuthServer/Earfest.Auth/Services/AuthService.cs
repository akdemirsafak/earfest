using System.Web;
using earfest.Shared.Base;
using earfest.Shared.Events;
using earfest.Shared.Models;
using Earfest.Auth.AbstractServices;
using Earfest.Auth.Entities;
using Earfest.Auth.Models.Auth;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Earfest.Auth.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IHttpContextAccessor _httpContext;

    public AuthService(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService,
        ISendEndpointProvider sendEndpointProvider,
        IHttpContextAccessor httpContext)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _sendEndpointProvider = sendEndpointProvider;
        _httpContext = httpContext;
    }

    public async Task<AppResult<NoContentDto>> ConfirmEmailAsync(ConfirmEmailRequest request)//User Register olduktan sonra mail adresine gelen maili onaylamak için kullanılacak.
    {
        var user = await _userManager.FindByIdAsync(request.userId); // jwt'den mail adresine veya id'sine erişerek burada user'ı alalım.
        if (user == null)
            return AppResult<NoContentDto>.Fail("No user found with this email");
        var result = await _userManager.ConfirmEmailAsync(user, token:request.Token);
        return result.Succeeded ? AppResult<NoContentDto>.Success() : AppResult<NoContentDto>.Fail("Failed to confirm email");
    }

    public async Task<AppResult<NoContentDto>> ForgotPasswordAsync(Models.Auth.ForgotPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return AppResult<NoContentDto>.Fail(new List<string> { "User not found" });
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var httpContextRequest = _httpContext.HttpContext?.Request;
        var baseUrl = $"{httpContextRequest.Scheme}://{httpContextRequest.Host}";


        var callbackUrl = $"{baseUrl}/api/auth/reset-password?userId={user.Id}&token={HttpUtility.UrlEncode(token)}";
        var emailBody = $"<a href='{callbackUrl}'>Reset Password</a>";


        var sendEndpoint=await _sendEndpointProvider.GetSendEndpoint(new System.Uri("queue:forgot-password-email-queue"));

        var userCreatedEvent=new ForgotPasswordEvent { To = user.Email, Subject = "Reset Password", Body = emailBody };
        await sendEndpoint.Send<ForgotPasswordEvent>(userCreatedEvent);

        return AppResult<NoContentDto>.Success();
    }

    public async Task<AppResult<AppTokenResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return AppResult<AppTokenResponse>.Fail("Invalid email or password",400);
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
            return AppResult<AppTokenResponse>.Fail("Invalid email or password",400);

        var token = await _tokenService.CreateTokenAsync(user);
        return AppResult<AppTokenResponse>.Success(token);
    }

    public async Task<AppResult<NoContentDto>> RegisterAsync(RegisterRequest request)
    {
        var user = new AppUser
        {
            Id = Guid.NewGuid().ToString(),
            Email = request.Email,
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };
        var result = await _userManager.CreateAsync(user, request.Password);

        if(!result.Succeeded)
            return AppResult<NoContentDto>.Fail(result.Errors.Select(x => x.Description).ToList());//
        var httpContextRequest = _httpContext.HttpContext?.Request;

        var baseUrl = $"{httpContextRequest.Scheme}://{httpContextRequest.Host}";

        var callbackUrl = $"{baseUrl}/api/auth/confirm-email?userId={user.Id}&token={HttpUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(user))}";
        var emailBody = $"<a href='{callbackUrl}'>Confirm Email</a>";

        var sendEndpoint=await _sendEndpointProvider.GetSendEndpoint(new System.Uri("queue:send-confirm-email-queue"));

        var userCreatedEvent=new ConfirmEmailEvent { To = user.Email, Subject = "Confirm Email", Body = emailBody };
        await sendEndpoint.Send<ConfirmEmailEvent>(userCreatedEvent);

        return AppResult<NoContentDto>.Success();
    }

    public async Task<AppResult<NoContentDto>> ResetPasswordAsync(string userId, string token, string password)
    {
        //var user = await _userManager.FindByEmailAsync(request.Email);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return AppResult<NoContentDto>.Fail(new List<string> { "User not found" });
        var result = await _userManager.ResetPasswordAsync(user, token, password);
        if (result.Succeeded)
            return AppResult<NoContentDto>.Success();
        return AppResult<NoContentDto>.Fail(result.Errors.Select(x => x.Description).ToList());
    }
}
