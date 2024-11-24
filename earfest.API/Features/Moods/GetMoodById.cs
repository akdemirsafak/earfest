using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Models.Moods;
using FluentValidation;
using Mapster;
using MediatR;

namespace earfest.API.Features.Moods;

public static class GetMoodById
{
    public record Query(string Id) : IRequest<AppResult<MoodResponse>>;
    public class QueryHandler(EarfestDbContext _context) : IRequestHandler<Query, AppResult<MoodResponse>>
    {
        public async Task<AppResult<MoodResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var mood = await _context.Moods.FindAsync(request.Id);
            return AppResult<MoodResponse>.Success(mood.Adapt<MoodResponse>(),200);
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
