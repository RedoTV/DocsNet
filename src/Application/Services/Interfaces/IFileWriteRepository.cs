namespace Application.Services.Interfaces;

public interface IFileWriteRepository<T>
{
    Task AddFileAsync(T file, CancellationToken cancellationToken);
    Task UpdateFileAsync(T file, CancellationToken cancellationToken);
    Task DeleteFileAsync(T file, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
