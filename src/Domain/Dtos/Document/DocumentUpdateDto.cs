namespace Domain.Dtos.Document;

public class DocumentUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
}