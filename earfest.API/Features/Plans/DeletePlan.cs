using earfest.API.Domain.DbContexts;
using earfest.Shared.Base;
using FluentValidation;
using MediatR;

namespace earfest.API.Features.Plans;

public static class DeletePlan
{
    public record Command(string Id) : IRequest<AppResult<NoContentDto>>;
    public class CommandHandler(EarfestDbContext _context) : IRequestHandler<Command, AppResult<NoContentDto>>
    {
        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var plan = await _context.Plans.FindAsync(request.Id);
            _context.Plans.Remove(plan);
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
