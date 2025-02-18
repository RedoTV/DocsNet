namespace Domain.Dtos.Document;

public class DocumentCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
}