using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class UserSignInDto
{
    [Required]
    [MinLength(4)]
    public required string? Name { get; set; }

    [Required]
    public required string? Password { get; set; }
}