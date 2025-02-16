namespace Domain.Entities;

public interface IUser
{
    public string? UserName { get; set; }
    public string? PasswordHash { get; set; }
}
