using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Models.Category;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace earfest.API.Features.Categories;

public static class CategoryGetAll
{
    public record Query() : IRequest<AppResult<List<CategoryResponse>>>;

    public class QueryHandler : IRequestHandler<Query, AppResult<List<CategoryResponse>>>
    {
        private readonly EarfestDbContext _dbContext;

        public QueryHandler(EarfestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AppResult<List<CategoryResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var categories = await _dbContext.Categories.ToListAsync();
            var response = categories.Adapt<List<CategoryResponse>>();
            return AppResult<List<CategoryResponse>>.Success(response);
        }
    }
}
