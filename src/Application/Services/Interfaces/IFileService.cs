namespace Application.Services.Interfaces;

public interface IFileService
{
    Task<string> SaveFileAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken);
    Task<bool> DeleteFileAsync(string filePath, CancellationToken cancellationToken);
}