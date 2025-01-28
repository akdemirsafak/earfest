using earfest.API.Domain.DbContexts;
using earfest.API.Models.Moods;
using earfest.Shared.Base;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace earfest.API.Features.Moods;

public static class GetMoods
{
    public record Query : IRequest<AppResult<List<MoodResponse>>>;
    public class QueryHandler(EarfestDbContext _context) : IRequestHandler<Query, AppResult<List<MoodResponse>>>
    {
        public async Task<AppResult<List<MoodResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var moods = await _context.Moods.ToListAsync();
            var response = moods.Adapt<List<MoodResponse>>();
            return AppResult<List<MoodResponse>>.Success(response,200);
        }
    }
}
