using Domain.Dtos.Document;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IDocumentService
{
    public Task UploadDocumentAsync(DocumentUploadDto document, Stream stream, CancellationToken cancellationToken);
    public Task RemoveDocumentAsync(DocumentRemoveDto document, CancellationToken cancellationToken);
    public Task UpdateDocumentAsync(DocumentUpdateDto document, CancellationToken cancellationToken);
}