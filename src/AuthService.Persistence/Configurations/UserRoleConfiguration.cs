<<<<<<< HEAD
﻿using AuthService.Core.Models;
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
=======
﻿namespace AuthService.Persistence.Configurations;

public class UserRoleConfiguration
{
    
>>>>>>> 65d69c9ca735abdf7fd91f28c094b5514ed5658d
}