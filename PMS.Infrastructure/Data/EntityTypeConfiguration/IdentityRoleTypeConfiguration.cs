using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PMS.Infrastructure.Data.EntityTypeConfiguration
{
    public class IdentityRoleTypeConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
            new IdentityRole
            {
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            },
            new IdentityRole
            {
                Name = "ProjectOwner",
                NormalizedName = "PROJECTOWNER"
            },
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
        }
    }
}