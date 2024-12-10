using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Models.Moods;
using FluentValidation;
using Mapster;
using MediatR;

namespace earfest.API.Features.Moods;

public static class GetMoodById
{
    public record Query(string Id) : IRequest<AppResult<MoodDetailsResponse>>;
    public class QueryHandler(EarfestDbContext _context) : IRequestHandler<Query, AppResult<MoodDetailsResponse>>
    {
        public async Task<AppResult<MoodDetailsResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var mood = await _context.Moods.FindAsync(request.Id);
            return AppResult<MoodDetailsResponse>.Success(mood.Adapt<MoodDetailsResponse>(), 200);
        }
    }

    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}
