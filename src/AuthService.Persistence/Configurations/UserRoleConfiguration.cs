using AuthService.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Persistence.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(ux => new { ux.UserId, ux.RoleId });
        builder.Property(u => u.User).IsRequired();
        builder.Property(r => r.Role).IsRequired();

        //Many-to-Many
    }
}