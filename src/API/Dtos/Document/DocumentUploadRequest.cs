namespace DocsNetAPI.Dtos.Document;

public class DocumentUploadRequest
{
    public string DocumentName { get; set; } = string.Empty;
    public string DocumentDescription { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
    public IFormFile File { get; set; } = null!;
}
