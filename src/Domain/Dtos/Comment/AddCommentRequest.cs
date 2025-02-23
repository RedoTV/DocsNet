namespace Domain.Dtos.Comment;

public class AddCommentRequest
{
    public int DocumentId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}