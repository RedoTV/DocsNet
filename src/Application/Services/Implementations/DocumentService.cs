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

    public Task UpdateDocumentAsync(DocumentUpdateDto document, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
