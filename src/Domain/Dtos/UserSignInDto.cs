namespace Domain.Dtos;

public class UserSignInDto
{
    public required string? Name { get; set; }
    public required string? Password { get; set; }
}