using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Domain.Entities;
using earfest.API.Models.Playlists;
using FluentValidation;
using Mapster;
using MediatR;

namespace earfest.API.Features.Playlists;

public static class CreatePlaylist
{
    public record Command(string Name, string Description, string ImageUrl) : IRequest<AppResult<PlaylistResponse>>;
    public class CommandHandler : IRequestHandler<Command, AppResult<PlaylistResponse>>
    {
        private readonly EarfestDbContext _context;
        public CommandHandler(EarfestDbContext context)
        {
            _context = context;
        }
        public async Task<AppResult<PlaylistResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var playlist = request.Adapt<Playlist>();
           
            await _context.Playlists.AddAsync(playlist);
            await _context.SaveChangesAsync(cancellationToken);

            return AppResult<PlaylistResponse>.Success(playlist.Adapt<PlaylistResponse>());
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(32);
            RuleFor(x => x.Description)
                .MaximumLength(256);

        }
    }

}
