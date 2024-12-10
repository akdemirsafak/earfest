using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Models.Moods;
using FluentValidation;
using Mapster;
using MediatR;

namespace earfest.API.Features.Moods;

public static class UpdateMood
{
    public record Command(string Id, UpdateMoodRequest Model) : IRequest<AppResult<MoodResponse>>;
    public class CommandHandler(EarfestDbContext _context) : IRequestHandler<Command, AppResult<MoodResponse>>
    {
        public async Task<AppResult<MoodResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var mood = await _context.Moods.FindAsync(request.Id);
            mood.Name = request.Model.Name;
            mood.Description = request.Model.Description;
            mood.ImageUrl = request.Model.ImageUrl;
            await _context.SaveChangesAsync(cancellationToken);
            return AppResult<MoodResponse>.Success(mood.Adapt<MoodResponse>(), 200);
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Model.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(32);
            RuleFor(x => x.Model.Description)
                .MaximumLength(256);
        }
    }
}
