using Application.Services.Interfaces;
using Domain.Dtos.Document;
using Domain.Entities;

namespace Application.Services.Implementations;

public class DocumentService : IDocumentService
{
    public Task RemoveDocumentAsync(DocumentRemoveDto document, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateDocumentAsync(DocumentUpdateDto document, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UploadDocumentAsync(DocumentUploadDto document, Stream stream, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
