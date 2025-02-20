using Domain.Dtos.File;

namespace Domain.Dtos.Document;

public class DocumentUploadDto
{
    public string DocumentName { get; set; } = string.Empty;
    public string DocumentDescription { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
    public FileData FileData { get; set; } = null!;
}