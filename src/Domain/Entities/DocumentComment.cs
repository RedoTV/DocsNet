using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class DocumentComment
{
    [Key]
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;

    public int DocumentId { get; set; }
    public string UserId { get; set; } = string.Empty;
}