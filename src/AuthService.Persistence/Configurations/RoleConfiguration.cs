<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
using AuthService.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(n => n.Name).IsRequired().HasMaxLength(128);
        
        //Many-To-Many
    }
=======
﻿namespace AuthService.Persistence.Configurations;

public class RoleConfiguration
{
    
>>>>>>> 65d69c9ca735abdf7fd91f28c094b5514ed5658d
}