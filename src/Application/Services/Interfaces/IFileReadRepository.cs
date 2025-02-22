namespace Application.Services.Interfaces;

public interface IFileReadRepository<T>
{
    Task<T?> GetFileAsync(int fileId, CancellationToken cancellationToken);
    Task<IEnumerable<T?>> GetUserFilesAsync(string userId, CancellationToken cancellationToken);
    Task<T?> GetUserFileAsync(int documentId, string userId);
    Task<T?> GetFileByShareLinkAsync(string shareLink, CancellationToken cancellationToken);
    Task<string?> GetShareLink(string userId, int documentId, CancellationToken cancellationToken);
}
