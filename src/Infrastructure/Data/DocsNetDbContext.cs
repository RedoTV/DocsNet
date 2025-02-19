using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

public class DocsNetDbContext : IdentityDbContext<User>
{
    public DocsNetDbContext(DbContextOptions<DocsNetDbContext> options) : base(options)
    { 
    }

    public DbSet<Document> Documents { get;set; }
    public DbSet<DocumentHistory> DocumentHistory { get;set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Document>()
            .HasOne<User>()
            .WithMany(u => u.Documents)
            .HasForeignKey(u => u.Id);

        builder.Entity<DocumentHistory>()
            .HasOne<User>()
            .WithMany(u => u.DocumentHistory);
            
        base.OnModelCreating(builder);
    }
}
