namespace earfest.API.Services;

public class FileService : IFileService
{
    public async Task UploadFileAsync(IFormFile file, string fileName) //returning imageUrl
    {
        if (file is not null && file.Length > 0)
        {
            //var extent = Path.GetExtension(file.FileName);
            //var randomName = ($"{Guid.NewGuid()}{extent}");
            var path=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/photos",fileName);
            using var stream=new FileStream(path,FileMode.Create);
            await file.CopyToAsync(stream, CancellationToken.None);
        }
    }
}
