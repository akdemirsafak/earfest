using earfest.Shared.Base;
using earfest.Shared.Events;
using Earfest.Auth.AbstractServices;
using Earfest.Auth.Entities;
using Earfest.Auth.Helpers;
using Earfest.Auth.Models.User;
using Mapster;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Earfest.Auth.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ICurrentUser _currentUser;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public UserService(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ICurrentUser currentUser,
        ISendEndpointProvider sendEndpointProvider)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _currentUser = currentUser;
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task<AppResult<NoContentDto>> ChangePasswordAsync(ChangePasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return AppResult<NoContentDto>.Fail(new List<string> { "User not found" });
        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);

            string emailBody = "Your password has been changed successfully.";

            var sendEndpoint=await _sendEndpointProvider.GetSendEndpoint(new System.Uri("queue:password-changed-email-queue"));
            var userCreatedEvent=new PasswordChangedEmailEvent { To = user.Email, Subject = "Reset Password", Body = emailBody };
            await sendEndpoint.Send<PasswordChangedEmailEvent>(userCreatedEvent);


            return AppResult<NoContentDto>.Success();
        }
        return AppResult<NoContentDto>.Fail(result.Errors.Select(x => x.Description).ToList());
    }

    public async Task<AppResult<List<UserResponse>>> GetUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        return AppResult<List<UserResponse>>.Success(users.Adapt<List<UserResponse>>());
    }

    public async Task<AppResult<NoContentDto>> LogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return AppResult<NoContentDto>.Success();
    }

    public async Task<AppResult<ProfileResponse>> ProfileAsync()
    {
        var users = await _userManager.FindByIdAsync(_currentUser.GetUserId);
        return AppResult<ProfileResponse>.Success(users.Adapt<ProfileResponse>());
    }

    public async Task<AppResult<UserResponse>> UpdateAsync(UpdateUserRequest request)
    {
        var user = await _userManager.FindByIdAsync(_currentUser.GetUserId);

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.BirthDate = request.BirthDate;

        await _userManager.UpdateAsync(user);
        return AppResult<UserResponse>.Success(user.Adapt<UserResponse>(), 200);
    }
}
