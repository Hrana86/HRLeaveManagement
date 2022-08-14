﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Identity.Configurations;
public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData
            (
            new IdentityRole
            {
                Id = "cac43a6e-f7bb-4448-baaf-ladd431ccbbf",
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            },
            new IdentityRole
            {
                Id = "cac43a8e-f7bb-4445-baaf-ladd431ffbbf",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            }
            );
    }
}