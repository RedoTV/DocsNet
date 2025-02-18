namespace Application.Services.Interfaces;

public interface IFileReadRepository<T>
{
    Task<T?> GetFileAsync(int fileId, CancellationToken cancellationToken);
    Task<IEnumerable<T?>> GetUserFilesAsync(string userId, CancellationToken cancellationToken);
}
