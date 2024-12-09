using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Domain.Entities;
using earfest.API.Helpers;
using earfest.API.Models.Category;
using earfest.API.Services;
using FluentValidation;
using Mapster;
using MediatR;

namespace earfest.API.Features.Categories;

public static class CreateCategory
{
    public record Command(string Name, string? Description, IFormFile image) : IRequest<AppResult<CategoryResponse>>;

    public class CommandHandler(EarfestDbContext _dbContext, IFileService _fileService) : IRequestHandler<Command, AppResult<CategoryResponse>>
    {

        public async Task<AppResult<CategoryResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = request.Adapt<Category>();
            var imageUrl = StringGenerator.GenerateRandomString(32);
            var imageExtent = Path.GetExtension(request.image.FileName);
            entity.ImageUrl = $"{imageUrl}{imageExtent}";
            await _dbContext.Categories.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            //File Server'a yükleme işlemi yapılacak dosya adı imageUrl olacak
            await _fileService.UploadFileAsync(request.image, entity.ImageUrl);

            return AppResult<CategoryResponse>.Success(entity.Adapt<CategoryResponse>(), 200);
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .Length(3, 32);
            RuleFor(x => x.Description)
                .MaximumLength(255);
        }
    }
}
