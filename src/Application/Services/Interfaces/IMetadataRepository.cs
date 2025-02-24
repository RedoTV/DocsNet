using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IMetadataRepository
{
    Task AddMetadataAsync(DocumentMetadata metadata, CancellationToken cancellationToken);
    Task UpdateMetadataAsync(int documentId, double latitude, double longitude, CancellationToken cancellationToken);
    Task<DocumentMetadata?> GetMetadataAsync(int documentId, CancellationToken cancellationToken);
}