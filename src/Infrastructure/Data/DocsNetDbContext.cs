using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

public class DocsNetDbContext : IdentityDbContext<User>
{
    public DocsNetDbContext(DbContextOptions<DocsNetDbContext> options) : base(options)
    {
    }

    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentHistory> DocumentHistory { get; set; }
    public DbSet<DocumentComment> DocumentComments { get; set; }
    public DbSet<DocumentMetadata> DocumentMetadata { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Document>()
            .HasOne<User>()
            .WithMany(u => u.Documents)
            .HasPrincipalKey(u => u.Id)
            .HasForeignKey(d => d.UserId);

        builder.Entity<DocumentHistory>()
            .HasOne<User>()
            .WithMany(u => u.DocumentHistory)
            .HasPrincipalKey(u => u.Id)
            .HasForeignKey(dh => dh.UserId);

        builder.Entity<DocumentComment>()
            .HasOne<Document>()
            .WithMany(d => d.Comments)
            .HasForeignKey(dc => dc.DocumentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<DocumentComment>()
            .HasOne<User>()
            .WithMany(u => u.Comments)
            .HasForeignKey(dc => dc.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<DocumentMetadata>()
            .HasOne<Document>()
            .WithOne(d => d.DocumentMetadata)
            .HasForeignKey<DocumentMetadata>(m => m.DocumentId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(builder);
    }
}
