using Application.Services.Interfaces;

namespace Application.Services.Implementations;

public class FileService : IFileService
{
    private readonly string _fileStoragePath;

    public FileService(string fileStoragePath)
    {
        _fileStoragePath = fileStoragePath;
    }

    public async Task<string> SaveFileAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken)
    {
        var filePath = Path.Combine(_fileStoragePath, fileName);

        if (!Directory.Exists(_fileStoragePath))
        {
            Directory.CreateDirectory(_fileStoragePath);
        }

        using (var fileStreamDestination = new FileStream(filePath, FileMode.Create))
        {
            await fileStream.CopyToAsync(fileStreamDestination, cancellationToken);
        }

        return filePath;
    }

    public async Task<bool> DeleteFileAsync(string filePath, CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }

            return false;
        });
    }
}
