using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Models.Category;
using FluentValidation;
using Mapster;
using MediatR;

namespace earfest.API.Features.Categories;

public static class CategoryUpdate
{
    public record Command(string Id, string Name, string? Description, string? ImageUrl) : IRequest<AppResult<CategoryResponse>>;

    public class CommandHandler(EarfestDbContext _dbContext) : IRequestHandler<Command, AppResult<CategoryResponse>>
    {
        public async Task<AppResult<CategoryResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories.FindAsync(request.Id);
            if (category is null)
                throw new Exception("Category not found");
            category.Name = request.Name;
            category.Description = request.Description;
            category.ImageUrl = request.ImageUrl;
            await _dbContext.SaveChangesAsync();

            return AppResult<CategoryResponse>.Success(category.Adapt<CategoryResponse>(), 200);
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .Length(3, 32);
            RuleFor(x => x.Description)
                .MaximumLength(255);
        }
    }
}
