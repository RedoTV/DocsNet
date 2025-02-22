using Application.Services.Interfaces;
using Domain.Dtos.File;
using Domain.Entities;

namespace Application.Helpers;

public class DocumentUpdater
{
    private readonly Document _document;

    public DocumentUpdater(Document document)
    {
        _document = document;
    }

    public DocumentUpdater UpdateName(string? name)
    {
        if (!string.IsNullOrEmpty(name))
            _document.Name = name;

        return this;
    }

    public DocumentUpdater UpdateDescription(string? description)
    {
        if (!string.IsNullOrEmpty(description))
            _document.Description = description;

        return this;
    }

    public DocumentUpdater UpdateExpirationDate(DateTime? expirationDate)
    {
        if (expirationDate.HasValue)
            _document.ExpirationDate = expirationDate.Value;

        return this;
    }

    public DocumentUpdater UpdateFile(FileData? fileData, IFileService fileService, CancellationToken cancellationToken)
    {
        if (fileData != null)
        {
            fileService.DeleteFileAsync(_document.FilePath, cancellationToken).Wait(cancellationToken);
            var newFilePath = fileService.SaveFileAsync(fileData, cancellationToken).Result;
            _document.FilePath = newFilePath;
            _document.ContentType = fileData.ContentType;
        }
        return this;
    }

    public Document Build()
    {
        return _document;
    }
}