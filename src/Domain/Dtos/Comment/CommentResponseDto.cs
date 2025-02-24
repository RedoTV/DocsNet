namespace Domain.Dtos.Comment;

public class CommentResponseDto
{
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
