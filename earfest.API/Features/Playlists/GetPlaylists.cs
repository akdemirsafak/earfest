using earfest.API.Domain.DbContexts;
using earfest.API.Models.Playlists;
using earfest.Shared.Base;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace earfest.API.Features.Playlists;

public static class GetPlaylists
{
    public record Query() : IRequest<AppResult<IList<PlaylistResponse>>>;
    public class QueryHandler(EarfestDbContext _context) : IRequestHandler<Query, AppResult<IList<PlaylistResponse>>>
    {
        public async Task<AppResult<IList<PlaylistResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var playlists = await _context.Playlists.ToListAsync(cancellationToken);
            return AppResult<IList<PlaylistResponse>>.Success(playlists.Adapt<IList<PlaylistResponse>>());
        }
    }
}
