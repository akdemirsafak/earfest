using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Domain.Entities;
using earfest.API.Models.Category;
using FluentValidation;
using Mapster;
using MediatR;

namespace earfest.API.Features.Categories;

public static class CreateCategory
{
    public record Command(string Name, string? Description, string? ImageUrl) : IRequest<AppResult<CategoryResponse>>;

    public class CommandHandler(EarfestDbContext _dbContext) : IRequestHandler<Command, AppResult<CategoryResponse>>
    {
        public async Task<AppResult<CategoryResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = request.Adapt<Category>();

            await _dbContext.Categories.AddAsync(entity);

            await _dbContext.SaveChangesAsync();
            return AppResult<CategoryResponse>.Success(entity.Adapt<CategoryResponse>(), 200);
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .Length(3, 32);
            RuleFor(x => x.Description)
                .MaximumLength(255);
        }
    }
}
