using Domain.Dtos.File;

namespace Application.Services.Interfaces;

public interface IFileService
{
    
    Task<string> SaveFileAsync(FileData fileData, CancellationToken cancellationToken);
    Task<bool> DeleteFileAsync(string filePath, CancellationToken cancellationToken);
}