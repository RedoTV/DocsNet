using Application.Services.Interfaces;
using Domain.Dtos.Document;
using Domain.Entities;

namespace Application.Services.Implementations;

public class DocumentService : IDocumentService
{
    private readonly IFileReadRepository<Document> _documentReadRepository;
    private readonly IFileWriteRepository<Document> _documentWriteRepository;
    private readonly IFileService _fileService;

    public DocumentService(
        IFileReadRepository<Document> documentReadRepository,
        IFileWriteRepository<Document> documentWriteRepository,
        IFileService fileService)
    {
        _documentReadRepository = documentReadRepository;
        _documentWriteRepository = documentWriteRepository;
        _fileService = fileService;
    }

    public async Task<Document> UploadDocumentAsync(DocumentUploadDto documentDto, string userId, CancellationToken cancellationToken)
    {
        var filePath = await _fileService.SaveFileAsync(documentDto.Stream, documentDto.DocumentName, documentDto.ContentType, cancellationToken);

        var document = new Document
        {
            Name = documentDto.DocumentName,
            FilePath = filePath,
            UserId = userId,
            ExpirationDate = DateTime.UtcNow.AddDays(30)
        };

        await _documentWriteRepository.AddFileAsync(document, cancellationToken);
        await _documentWriteRepository.SaveChangesAsync(cancellationToken);

        return document;
    }

    public async Task<Document?> GetDocumentByIdAsync(int documentId, CancellationToken cancellationToken)
    {
        return await _documentReadRepository.GetFileAsync(documentId, cancellationToken);
    }

    public async Task<bool> RemoveDocumentAsync(int documentId, string userId, CancellationToken cancellationToken)
    {
        var document = await _documentReadRepository.GetFileAsync(documentId, cancellationToken);

        if (document == null || document.UserId != userId)
        {
            return false;
        }

        await _fileService.DeleteFileAsync(document.FilePath, cancellationToken);

        await _documentWriteRepository.DeleteFileAsync(document, cancellationToken);
        await _documentWriteRepository.SaveChangesAsync(cancellationToken);

        return true;
    }

    public Task UpdateDocumentAsync(DocumentUpdateDto document, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
