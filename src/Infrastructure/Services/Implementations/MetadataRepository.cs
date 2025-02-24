using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Implementations;

public class MetadataRepository : IMetadataRepository
{
    private readonly DocsNetDbContext _docsNetDb;

    public MetadataRepository(DocsNetDbContext docsNetDb)
    {
        _docsNetDb = docsNetDb;
    }

    public async Task AddMetadataAsync(DocumentMetadata metadata, CancellationToken cancellationToken)
    {
        await _docsNetDb.DocumentMetadata.AddAsync(metadata, cancellationToken);

        await _docsNetDb.SaveChangesAsync(cancellationToken);
    }

    public async Task<DocumentMetadata?> GetMetadataAsync(int documentId, CancellationToken cancellationToken)
    {
        return await _docsNetDb.DocumentMetadata
            .Where(dm => dm.DocumentId == documentId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task UpdateMetadataAsync(int documentId, double latitude, double longitude, CancellationToken cancellationToken)
    {
        var document = await _docsNetDb.Documents
            .Include(d => d.DocumentMetadata)
            .Where(d => d.Id == documentId)
            .FirstOrDefaultAsync();

        if (document is null)
            throw new ArgumentException($"Document with id:{documentId} not found");

        document.DocumentMetadata.Latitude = latitude;
        document.DocumentMetadata.Longitude = longitude;
        document.DocumentMetadata.CreatedAt = DateTime.UtcNow;
        await _docsNetDb.SaveChangesAsync();
    }
}
