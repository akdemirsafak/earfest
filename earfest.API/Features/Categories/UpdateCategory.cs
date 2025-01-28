using earfest.API.Domain.DbContexts;
using earfest.API.Models.Category;
using earfest.API.Models.Moods;
using earfest.Shared.Base;
using FluentValidation;
using Mapster;
using MediatR;

namespace earfest.API.Features.Categories;

public static class UpdateCategory
{
    public record Command(string Id, UpdateMoodRequest Model) : IRequest<AppResult<CategoryResponse>>;

    public class CommandHandler(EarfestDbContext _dbContext) : IRequestHandler<Command, AppResult<CategoryResponse>>
    {
        public async Task<AppResult<CategoryResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories.FindAsync(request.Id);
            if (category is null)
                throw new Exception("Category not found");
            category.Name = request.Model.Name;
            category.Description = request.Model.Description;
            category.ImageUrl = request.Model.ImageUrl;
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
            RuleFor(x => x.Model.Name)
                .NotNull()
                .NotEmpty()
                .Length(3, 32);
            RuleFor(x => x.Model.Description)
                .MaximumLength(255);
        }
    }
}
