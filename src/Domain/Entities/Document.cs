using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Document
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string ShareLink { get; set; } = string.Empty;

    public ICollection<DocumentComment> Comments { get; set; } = new List<DocumentComment>();
    public string UserId { get; set; } = string.Empty;
}