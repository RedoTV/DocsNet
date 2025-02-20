using Domain.Dtos.Document;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IDocumentService
{
    Task<Document?> GetDocumentByIdAsync(int documentId, CancellationToken cancellationToken);
    Task<Document> UploadDocumentAsync(DocumentUploadDto documentDto, string userId, CancellationToken cancellationToken);
    Task<bool> RemoveDocumentAsync(int documentId, string userId, CancellationToken cancellationToken);
    Task UpdateDocumentAsync(DocumentUpdateDto document, CancellationToken cancellationToken);
}