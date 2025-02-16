using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class DocumentComment
{
    [Key]
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;

    public required int DocumentId { get; set; }
    public required string UserId { get; set; }
}
