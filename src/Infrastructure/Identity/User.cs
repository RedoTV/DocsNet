using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class User : IdentityUser
{
    public ICollection<Document> Documents { get; set; } = new List<Document>();
    public ICollection<DocumentHistory> DocumentHistory { get; set; } = new List<DocumentHistory>();
    public ICollection<DocumentComment> Comments { get; set; } = new List<DocumentComment>();
}
