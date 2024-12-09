namespace earfest.API.Services;

public interface IFileService
{
    Task UploadFileAsync(IFormFile file, string fileName);
}