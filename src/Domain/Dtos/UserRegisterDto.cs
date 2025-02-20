using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class UserRegisterDto
{
    [Required]
    public required string? Name { get; set; }

    [Required]
    [MinLength(6)]
    public required string? Password { get; set; }
}
