namespace DocsNetAPI.Dtos.Document;

public class DocumentUpdateRequest
{
    public int Id { get; set; }
    public string? DocumentName { get; set; }
    public string? DocumentDescription { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public IFormFile? File { get; set; }
}
