using earfest.API.Domain.DbContexts;
using earfest.Shared.Base;
using FluentValidation;
using MediatR;

namespace earfest.API.Features.Contents;

public static class UpdateContent
{
    public record Command(string Id, UpdateContentRequest Model) : IRequest<AppResult<NoContentDto>>;

    public record UpdateContentRequest(string Name,
        string Description,
        string? ImageUrl,
        string? AudioUrl,
        string? VideoUrl,
        string? Lyrics);
    public class CommandHandler(EarfestDbContext _dbContext) : IRequestHandler<Command, AppResult<NoContentDto>>
    {
        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var content = await _dbContext.Contents.FindAsync(request.Id);
            content.Name = request.Model.Name;
            content.Description = request.Model.Description;
            content.ImageUrl = request.Model.ImageUrl;
            content.AudioUrl = request.Model.AudioUrl;
            content.VideoUrl = request.Model.VideoUrl;
            content.Lyrics = request.Model.Lyrics;
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
            RuleFor(x => x.Model.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Model.Description)
                .NotNull()
                .NotEmpty();
        }
    }
}
