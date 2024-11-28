using earfest.API.Base;
using earfest.API.Domain.Entities;
using earfest.API.Models.Auth;
using earfest.API.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace earfest.API.Features.Auths;

public static class Login
{
    public record Query(string Email, string Password) : IRequest<AppResult<AppTokenResponse>>;
    public class QueryHandler : IRequestHandler<Query, AppResult<AppTokenResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        public QueryHandler(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task<AppResult<AppTokenResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return AppResult<AppTokenResponse>.Fail("Invalid email or password");
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded) return AppResult<AppTokenResponse>.Fail("Invalid email or password");

            var token = await _tokenService.CreateTokenAsync(user);
            return AppResult<AppTokenResponse>.Success(token);
        }
    }
    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
