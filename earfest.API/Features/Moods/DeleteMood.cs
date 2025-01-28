using earfest.API.Domain.DbContexts;
using earfest.Shared.Base;
using FluentValidation;
using MediatR;

namespace earfest.API.Features.Moods;

public static class DeleteMood
{
    public record Command(string Id) : IRequest<AppResult<NoContentDto>>;
    public class CommandHandler(EarfestDbContext _context) : IRequestHandler<Command, AppResult<NoContentDto>>
    {
        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var mood = await _context.Moods.FindAsync(request.Id);
            _context.Moods.Remove(mood);
            await _context.SaveChangesAsync(cancellationToken);
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
