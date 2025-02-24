using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IMetadataService
{
    Task SetMetadataAsync(int documentId, string userId, DocumentMetadata metadata, CancellationToken cancellationToken);
    Task<DocumentMetadata> GetMetadataAsync(int documentId, string userId, CancellationToken cancellationToken);
}
