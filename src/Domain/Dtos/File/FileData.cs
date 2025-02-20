namespace Domain.Dtos.File;

public class FileData
{
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;

    public Stream FileStream { get; set; } = null!;
}