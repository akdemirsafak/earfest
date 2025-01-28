using earfest.API.Domain.DbContexts;
using earfest.Shared.Base;
using FluentValidation;
using MediatR;

namespace earfest.API.Features.Playlists;

public static class UpdatePlaylist
{
    public record Command(string Id, string Name, string? Description, string? ImageUrl) : IRequest<AppResult<NoContentDto>>;
    public class CommandHandler(EarfestDbContext _context) : IRequestHandler<Command, AppResult<NoContentDto>>
    {
        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var playlist = await _context.Playlists.FindAsync(request.Id);
            playlist.Name = request.Name;
            playlist.Description = request.Description;
            playlist.ImageUrl = request.ImageUrl;
            await _context.SaveChangesAsync(cancellationToken);
            return AppResult<NoContentDto>.Success();
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(32);
            RuleFor(x => x.Description)
                .MaximumLength(256);
        }
    }
}
