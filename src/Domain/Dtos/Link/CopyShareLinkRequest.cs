namespace Domain.Dtos.Link;

public class ShareLinkPostDto
{
    public string ShareLink { get; set; } = string.Empty;
    public DateTime? NewExpirationDate { get; set; }
}
