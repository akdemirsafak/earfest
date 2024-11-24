using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Domain.Entities;
using earfest.API.Models.Moods;
using FluentValidation;
using Mapster;
using MediatR;

namespace earfest.API.Features.Moods;

public static class CreateMood
{
    public record Command(string Name, string Description, string ImageUrl) : IRequest<AppResult<MoodResponse>>;
    public class CommandHandler(EarfestDbContext _context) : IRequestHandler<Command, AppResult<MoodResponse>>
    {
        public async Task<AppResult<MoodResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var mood = request.Adapt<Mood>();
            await _context.Moods.AddAsync(mood);
            await _context.SaveChangesAsync(cancellationToken);
            return AppResult<MoodResponse>.Success(mood.Adapt<MoodResponse>(), 201);
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(32);
            RuleFor(x => x.Description)
                .MaximumLength(256);
        }
    }
}
