using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class User : IdentityUser, IUser
{
    public ICollection<Document> Documents { get; set; } = new List<Document>();
    public ICollection<DocumentHistory> DocumentHistory { get; set; } = new List<DocumentHistory>();
}
