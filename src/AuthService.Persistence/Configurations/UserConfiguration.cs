using AuthService.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(n => n.FullName).HasMaxLength(128).IsRequired();
        // builder.Property(s => s.Surname).HasMaxLength(128).IsRequired();
        // builder.Property(u => u.Username).HasMaxLength(128).IsRequired();
        builder.Property(e => e.Email).HasMaxLength(128).IsRequired();
        builder.HasIndex(e => e.Email).IsUnique();
        builder.Property(p => p.Password).HasMaxLength(128).IsRequired(false);
    }
}