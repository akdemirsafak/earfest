using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Models.Category;
using FluentValidation;
using Mapster;
using MediatR;

namespace earfest.API.Features.Categories;

public static class CategoryGetById
{
    public record Query(string Id) : IRequest<AppResult<CategoryResponse>>;
    public class QueryHandler : IRequestHandler<Query, AppResult<CategoryResponse>>
    {
        private readonly EarfestDbContext _context;

        public QueryHandler(EarfestDbContext context)
        {
            _context = context;
        }

        public async Task<AppResult<CategoryResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var response = await _context.Categories.FindAsync(request.Id);
            if (response is null)
                return AppResult<CategoryResponse>.Fail("Category not found");

            CategoryResponse result = response.Adapt<CategoryResponse>();
            return AppResult<CategoryResponse>.Success(result);

        }
        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }


    }
}
