using earfest.API.Domain.DbContexts;
using earfest.Shared.Base;
using FluentValidation;
using MediatR;

namespace earfest.API.Features.Playlists;

public static class AddContentToPlaylist
{
    public record Command(string PlaylistId, string ContentId) : IRequest<AppResult<NoContentDto>>;
    public class CommandHandler(EarfestDbContext _context) : IRequestHandler<Command, AppResult<NoContentDto>>
    {
        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var playlist = await _context.Playlists.FindAsync(request.PlaylistId);
            var content = await _context.Contents.FindAsync(request.ContentId);
            playlist.Contents.Add(content);
            await _context.SaveChangesAsync(cancellationToken);
            return AppResult<NoContentDto>.Success();
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.PlaylistId).NotEmpty();
            RuleFor(x => x.ContentId).NotEmpty();
        }
    }
}
