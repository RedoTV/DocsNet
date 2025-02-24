namespace Domain.Dtos.Comment;

public class AddCommentRequestDto
{
    public int DocumentId { get; set; }
    public string Text { get; set; } = string.Empty;
}