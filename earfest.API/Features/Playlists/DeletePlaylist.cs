using earfest.API.Domain.DbContexts;
using earfest.Shared.Base;
using FluentValidation;
using MediatR;

namespace earfest.API.Features.Playlists;

public static class DeletePlaylist
{
    public record Command(string Id) : IRequest<AppResult<NoContentDto>>;
    public class CommandHandler(EarfestDbContext _context) : IRequestHandler<Command, AppResult<NoContentDto>>
    { 
        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var playlist = await _context.Playlists.FindAsync(request.Id);

            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync(cancellationToken);
            return AppResult<NoContentDto>.Success();
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
