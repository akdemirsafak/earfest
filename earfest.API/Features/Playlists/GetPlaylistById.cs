using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Models.Playlists;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace earfest.API.Features.Playlists;

public static class GetPlaylistById 
{
    public record Query(string Id) : IRequest<AppResult<PlaylistByIdResponse>>;
    public class QueryHandler(EarfestDbContext _dbContext) : IRequestHandler<Query, AppResult<PlaylistByIdResponse>>
    {
        public async Task<AppResult<PlaylistByIdResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var playlist = await _dbContext.Playlists
                .Include(x=>x.Contents)
                .FirstOrDefaultAsync(x=>x.Id==request.Id);
           
            return AppResult<PlaylistByIdResponse>.Success(playlist.Adapt<PlaylistByIdResponse>());
        }
    }

    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}
