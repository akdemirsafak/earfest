using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using FluentValidation;
using MediatR;

namespace earfest.API.Features.Categories;

public static class CategoryDelete
{
    public record Command(string id) : IRequest<AppResult<NoContentDto>>;

    public class CommandHandler : IRequestHandler<Command, AppResult<NoContentDto>>
    {
        private readonly EarfestDbContext _dbContext;

        public CommandHandler(EarfestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Categories.FindAsync(request.id);
            if (entity is null)
                return AppResult<NoContentDto>.Fail("Category not found.");

            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return AppResult<NoContentDto>.Success();
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.id).NotEmpty();
            }
        }

    }
}
