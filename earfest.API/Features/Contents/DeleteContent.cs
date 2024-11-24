using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using FluentValidation;
using MediatR;

namespace earfest.API.Features.Contents;

public static class DeleteContent
{
    public record Command(string Id) : IRequest<AppResult<NoContentDto>>;

    public class CommandHandler(EarfestDbContext _dbContext) : IRequestHandler<Command, AppResult<NoContentDto>>
    {
        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var content = await _dbContext.Contents.FindAsync(request.Id);
            _dbContext.Contents.Remove(content);
            await _dbContext.SaveChangesAsync();
            return AppResult<NoContentDto>.Success(204);
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}
