namespace Domain.Entities;

public class DocumentMetadata
{
    public int Id { get; set; }
    public int DocumentId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
