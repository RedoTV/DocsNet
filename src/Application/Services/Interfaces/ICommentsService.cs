using Domain.Entities;

namespace Application.Services.Interfaces;

public interface ICommentsService
{
    Task AddCommentAsync(int documentId, string userId, string text, CancellationToken cancellationToken);
    Task<List<DocumentComment>> GetCommentsByDocumentIdAsync(int documentId, string userId, CancellationToken cancellationToken);
}
