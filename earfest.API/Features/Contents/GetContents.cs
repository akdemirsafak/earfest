using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Models.Contents;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace earfest.API.Features.Contents;

public static class GetContents
{
    public record Query() : IRequest<AppResult<List<ContentResponse>>>;
    public class QueryHandler(EarfestDbContext _dbContext) : IRequestHandler<Query, AppResult<List<ContentResponse>>>
    {
        public async Task<AppResult<List<ContentResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var contents = await _dbContext.Contents.ToListAsync();
            var response= contents.Adapt<List<ContentResponse>>();                
            return AppResult<List<ContentResponse>>.Success(response);
        }
    }
}
