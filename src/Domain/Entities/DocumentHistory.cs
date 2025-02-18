using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class DocumentHistory
{
    [Key]
    public int Id { get; set; }

    // archived data from document
    public required int DocumentId { get; set; }
    public required string DocumentName { get; set; }
    public string Description { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;

    // date when document should be deleted from history
    public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddDays(30);

    public string UserId { get; set; } = string.Empty;
}
