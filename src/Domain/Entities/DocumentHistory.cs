using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class DocumentHistory
{
    [Key]
    public required int Id { get; set; }

    // archived data from document
    public required int DocumentId { get; set; }
    public required string DocumentName { get; set;} 
    public string Description { get; set;} = string.Empty;
    public string FilePath { get; set;} = string.Empty;

    // date when document moved to history
    public DateTime TransferDate { get; set; } = DateTime.Now;

    public required string UserId { get; set; }
}
