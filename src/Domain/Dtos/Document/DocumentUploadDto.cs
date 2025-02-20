namespace Domain.Dtos.Document;

public class DocumentUploadDto
{
    public string DocumentName { get; set; } = string.Empty;
    public Stream Stream { get; set; } = Stream.Null;
    public string ContentType { get; set; } = string.Empty;
}