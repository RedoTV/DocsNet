using System.Security.Cryptography;
using System.Text;
using Application.Helpers;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Dtos.Document;
using Domain.Entities;

namespace Application.Services.Implementations;

public class DocumentService : IDocumentService
{
    private readonly IFileReadRepository<Document> _documentReadRepository;
    private readonly IFileWriteRepository<Document> _documentWriteRepository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public DocumentService(
        IFileReadRepository<Document> documentReadRepository,
        IFileWriteRepository<Document> documentWriteRepository,
        IFileService fileService,
        IMapper mapper)
    {
        _documentReadRepository = documentReadRepository;
        _documentWriteRepository = documentWriteRepository;
        _fileService = fileService;
        _mapper = mapper;
    }

    public async Task<Document> UploadDocumentAsync(
        DocumentUploadDto documentDto,
        string userId,
        CancellationToken cancellationToken)
    {
        var filePath = await _fileService.SaveFileAsync(documentDto.FileData, cancellationToken);

        var document = _mapper.Map<Document>(documentDto);
        document.UserId = userId;
        document.FilePath = filePath;
        document.ContentType = documentDto.FileData.ContentType;
        document.ShareLink = await GenerateShareLinkAsync(document.ShareLink, cancellationToken);

        await _documentWriteRepository
            .AddFileAsync(document, cancellationToken);
        await _documentWriteRepository.SaveChangesAsync(cancellationToken);

        return document;
    }

    public async Task<Document?> GetDocumentByIdAsync(int documentId, CancellationToken cancellationToken)
    {
        return await _documentReadRepository
            .GetFileAsync(documentId, cancellationToken);
    }

    public async Task<bool> RemoveDocumentAsync(int documentId, string userId, CancellationToken cancellationToken)
    {
        var document = await _documentReadRepository.GetFileAsync(documentId, cancellationToken);

        if (document is null || document.UserId != userId)
        {
            return false;
        }

        await _fileService.DeleteFileAsync(document.FilePath, cancellationToken);

        await _documentWriteRepository.DeleteFileAsync(document, cancellationToken);
        await _documentWriteRepository.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task UpdateDocumentAsync(DocumentUpdateDto documentDto, string userId, CancellationToken cancellationToken)
    {
        var existingDocument = await _documentReadRepository.GetFileAsync(documentDto.Id, cancellationToken);
        if (existingDocument is null)
            throw new ArgumentException("Document not found");

        if (existingDocument.UserId != userId)
            throw new UnauthorizedAccessException("You are not authorized to update this document");

        var updatedDocument = new DocumentUpdater(existingDocument)
            .UpdateName(documentDto.DocumentName)
            .UpdateDescription(documentDto.DocumentDescription)
            .UpdateExpirationDate(documentDto.ExpirationDate)
            .UpdateFile(documentDto.FileData, _fileService, cancellationToken)
            .Build();

        await _documentWriteRepository.UpdateFileAsync(updatedDocument, cancellationToken);
        await _documentWriteRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task<string> GenerateShareLinkAsync(string documentName, CancellationToken cancellationToken)
    {
        string shareLink = "";
        await Task.Run(() =>
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(documentName));
            shareLink = Convert.ToBase64String(hashBytes).Replace("/", "_").Replace("+", "-");
        }, cancellationToken);

        return shareLink;
    }

    public async Task<Document> CopyDocumentByShareLinkAsync(string shareLink, string newUserId, DateTime? newExpirationDate, CancellationToken cancellationToken)
    {
        var originalDocument = await _documentReadRepository
            .GetFileByShareLinkAsync(shareLink, cancellationToken);

        if (originalDocument == null || originalDocument.ShareLink != shareLink)
        {
            throw new ArgumentException("Invalid share link");
        }

        var copiedDocument = originalDocument.DeepCopy();
        copiedDocument.ExpirationDate = newExpirationDate ?? originalDocument.ExpirationDate;

        await _documentWriteRepository.AddFileAsync(copiedDocument, cancellationToken);
        await _documentWriteRepository.SaveChangesAsync(cancellationToken);

        return copiedDocument;
    }

    public async Task<string?> GetShareLinkAsync(string userId, int documentId, CancellationToken cancellationToken)
    {
        return await _documentReadRepository.GetShareLink(userId, documentId, cancellationToken);
    }
}
