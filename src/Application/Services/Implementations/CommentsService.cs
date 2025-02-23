using Application.Services.Interfaces;
using Domain.Entities;

namespace Application.Services.Implementations;

public class CommentsService : ICommentsService
{
    private readonly ICommentsRepository _commentsRepository;

    public CommentsService(ICommentsRepository commentsRepository)
    {
        _commentsRepository = commentsRepository;
    }

    public async Task AddCommentAsync(int documentId, string userId, string text, CancellationToken cancellationToken)
    {
        var comment = new DocumentComment
        {
            DocumentId = documentId,
            UserId = userId,
            Text = text
        };

        await _commentsRepository.AddCommentAsync(comment, cancellationToken);
    }

    public async Task<List<DocumentComment>> GetCommentsByDocumentIdAsync(int documentId, CancellationToken cancellationToken)
    {
        return await _commentsRepository.GetCommentsByDocumentIdAsync(documentId, cancellationToken);
    }
}