﻿using earfest.API.Base;
using earfest.API.Domain.Entities;
using earfest.API.Helpers;
using earfest.API.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace earfest.API.Features.Auths;

public static class VerifyEmail
{
    public record Command(string Token) : IRequest<AppResult<NoContentDto>>;

    public class CommandHandler : IRequestHandler<Command, AppResult<NoContentDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ICurrentUser _currentUser;
        public CommandHandler(UserManager<AppUser> userManager,
            ITokenService tokenService,
            ICurrentUser currentUser)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _currentUser = currentUser;
        }
        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(_currentUser.GetEmail); // jwt'den mail adresine veya id'sine erişerek burada user'ı alalım.
            if (user == null) return AppResult<NoContentDto>.Fail("No user found with this email");
            var result = await _userManager.ConfirmEmailAsync(user, token:request.Token);
            return result.Succeeded ? AppResult<NoContentDto>.Success() : AppResult<NoContentDto>.Fail("Failed to verify email");
        }
    }
    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
