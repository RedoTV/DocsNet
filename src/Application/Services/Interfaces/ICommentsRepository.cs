using Domain.Entities;

namespace Application.Services.Interfaces;

public interface ICommentsRepository
{
    Task AddCommentAsync(DocumentComment comment, CancellationToken cancellationToken);
    Task<List<DocumentComment>> GetCommentsByDocumentIdAsync(int documentId, CancellationToken cancellationToken);
}