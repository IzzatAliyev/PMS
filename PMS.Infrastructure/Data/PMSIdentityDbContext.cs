using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Infrastructure.Entities;

namespace PMS.Infrastructure.Data;

public class PMSIdentityDbContext : IdentityDbContext<User>
{
    public PMSIdentityDbContext(DbContextOptions<PMSIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserEntityTypeConfiguration());

        var hasher = new PasswordHasher<IdentityUser>();

        var admin = new User
        {
            FirstName = "Admin",
            LastName = "Adminov",
            Email = "admin@gmail.com",
            UserName = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            NormalizedUserName = "ADMIN@GMAIL.COM",
        };
        var employee = new User
        {
            FirstName = "Employee",
            LastName = "Employov",
            Email = "employee@gmail.com",
            UserName = "employee@gmail.com",
            NormalizedEmail = "EMPLOYEE@GMAIL.COM",
            NormalizedUserName = "EMPLOYEE@GMAIL.COM",
        };

        var identityAdmin = new IdentityUser(admin.Id.ToString());
        var identityEmployee = new IdentityUser(employee.Id.ToString());

        admin.PasswordHash = hasher.HashPassword(identityAdmin, "Admin123!");
        employee.PasswordHash = hasher.HashPassword(identityEmployee, "Employee123!");

        builder.Entity<User>().HasData(admin, employee);

        var roles = new List<IdentityRole>()
            {
                new() { Name = "Admin", NormalizedName = "ADMIN" },
                new() { Name = "Employee", NormalizedName = "EMPLOYEE" }
            };
        builder.Entity<IdentityRole>().HasData(roles);

         builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = admin.Id,
                    RoleId =
                    roles.First(q => q.Name == "Admin").Id,
                },
                new IdentityUserRole<string>
                {
                    UserId = employee.Id,
                    RoleId =
                    roles.First(q => q.Name == "Employee").Id,
                });
    }

    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(255);
            builder.Property(u => u.LastName).HasMaxLength(255);
        }
    }
}
