using Application.Services.Interfaces;
using Domain.Entities;

namespace Application.Services.Implementations;

public class MetadataService : IMetadataService
{
    private readonly IMetadataRepository _metadataRepository;
    private readonly IDocumentService _documentService;

    public MetadataService(IDocumentService documentService, IMetadataRepository metadataRepository)
    {
        _metadataRepository = metadataRepository;
        _documentService = documentService;
    }

    public async Task SetMetadataAsync(int documentId, string userId, DocumentMetadata metadata, CancellationToken cancellationToken)
    {
        await CheckUserAccess(documentId, userId, cancellationToken);

        var document = await _documentService.GetDocumentByIdAsync(documentId, cancellationToken);

        if (document!.DocumentMetadata == null)
        {
            document.DocumentMetadata = new DocumentMetadata
            {
                Id = 0,
                DocumentId = documentId,
                Latitude = metadata.Latitude,
                Longitude = metadata.Longitude
            };

            await _metadataRepository.AddMetadataAsync(document.DocumentMetadata, cancellationToken);
        }
        else
        {
            await _metadataRepository.UpdateMetadataAsync(documentId, metadata.Latitude, metadata.Longitude, cancellationToken);
        }
    }

    public async Task<DocumentMetadata> GetMetadataAsync(int documentId, string userId, CancellationToken cancellationToken)
    {
        await CheckUserAccess(documentId, userId, cancellationToken);

        var documentMetadata = await _metadataRepository.GetMetadataAsync(documentId, cancellationToken);
        if (documentMetadata is null)
            throw new ArgumentException("Document metadata not found");

        return documentMetadata;
    }

    private async Task CheckUserAccess(int documentId, string userId, CancellationToken cancellationToken)
    {
        var document = await _documentService.GetDocumentByIdAsync(documentId, cancellationToken);
        if (document is null)
            throw new ArgumentException("Document not found");

        if (document.UserId != userId)
            throw new ArgumentException("User does not have access");
    }
}
