using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Models.Category;
using FluentValidation;
using Mapster;
using MediatR;

namespace earfest.API.Features.Categories;

public static class GetCategoryById
{
    public record Query(string Id) : IRequest<AppResult<CategoryDetailsResponse>>;
    public class QueryHandler : IRequestHandler<Query, AppResult<CategoryDetailsResponse>>
    {
        private readonly EarfestDbContext _context;

        public QueryHandler(EarfestDbContext context)
        {
            _context = context;
        }

        public async Task<AppResult<CategoryDetailsResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var response = await _context.Categories.FindAsync(request.Id);
            if (response is null)
                return AppResult<CategoryDetailsResponse>.Fail("Category not found");

            CategoryDetailsResponse result = response.Adapt<CategoryDetailsResponse>();
            return AppResult<CategoryDetailsResponse>.Success(result);

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
