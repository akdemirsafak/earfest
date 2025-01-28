using earfest.API.Domain.DbContexts;
using earfest.API.Domain.Entities;
using earfest.API.Models.Contents;
using earfest.Shared.Base;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace earfest.API.Features.Contents;

public static class CreateContent
{
    public record Command(string Name,
        IList<string> MoodIds, 
        IList<string> CategoryIds, 
        string? Description,
        string? ImageUrl, 
        string? AudioUrl, 
        string? VideoUrl,
        string? Lyrics) : IRequest<AppResult<ContentResponse>>;
    public class CommandHandler(EarfestDbContext _dbContext) : IRequestHandler<Command, AppResult<ContentResponse>>
    {
        public async Task<AppResult<ContentResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var moods = await _dbContext.Moods.Where(x => request.MoodIds.Contains(x.Id)).ToListAsync();
            var categories = await _dbContext.Categories.Where(x => request.CategoryIds.Contains(x.Id)).ToListAsync();
            var content = new Content
            {
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                AudioUrl = request.AudioUrl,
                VideoUrl = request.VideoUrl,
                Lyrics = request.Lyrics,
                Moods = moods,
                Categories = categories
            };
            await _dbContext.Contents.AddAsync(content);
            await _dbContext.SaveChangesAsync();
            var response = content.Adapt<ContentResponse>();
            return AppResult<ContentResponse>.Success(response);
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.MoodIds)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.CategoryIds)
                .NotNull()
                .NotEmpty();
        }
    }
}
