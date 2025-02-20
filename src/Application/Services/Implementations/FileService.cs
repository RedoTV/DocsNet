using Application.Services.Interfaces;
using Domain.Dtos.File;

namespace Application.Services.Implementations;

public class FileService : IFileService
{
    private readonly string _fileStoragePath;

    public FileService(string fileStoragePath)
    {
        _fileStoragePath = fileStoragePath;
        EnsureDirectoryExists();
    }

    public async Task<string> SaveFileAsync(FileData fileData, CancellationToken cancellationToken)
    {
        var fileGuid = GenerateGuidFileName(fileData.FileName);
        var filePath = Path.Combine(_fileStoragePath, fileGuid);

        using (var fileStreamDestination = new FileStream(filePath, FileMode.Create))
        {
            await fileData.FileStream.CopyToAsync(fileStreamDestination, cancellationToken);
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

    private string GenerateGuidFileName(string originalFileName)
    {
        var extension = Path.GetExtension(originalFileName);
        return string.Concat(Guid.NewGuid().ToString("N"), extension);
    }

    private void EnsureDirectoryExists()
    {
        if (!Directory.Exists(_fileStoragePath))
            Directory.CreateDirectory(_fileStoragePath);
    }
}
