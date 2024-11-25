﻿using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Models.Contents;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace earfest.API.Features.Contents;

public static class GetContentById
{
    public record Query(string Id) : IRequest<AppResult<ContentResponse>>;
    public class QueryHandler(EarfestDbContext _context) : IRequestHandler<Query, AppResult<ContentResponse>>
    {
        public async Task<AppResult<ContentResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var content = await _context.Contents
                .Include(x => x.Artists)
                .Include(x => x.Categories)
                .Include(x => x.Moods)
                .SingleOrDefaultAsync(x => x.Id == request.Id);
            //.Include(x => x.ContentTags)
            //.ThenInclude(x => x.Tag)
            //.FirstOrDefaultAsync(x => x.Id == request.Id);
            var response = content.Adapt<ContentResponse>();
            return AppResult<ContentResponse>.Success(response);
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