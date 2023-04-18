using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PMS.Infrastructure.Entities;
using PMS.Infrastructure.Data.EntityTypeConfiguration;

namespace PMS.Infrastructure.Data
{
    public class PMSIdentityDbContext : IdentityDbContext
    {
        public PMSIdentityDbContext(DbContextOptions<PMSIdentityDbContext> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new IdentityRoleTypeConfiguration());
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });
        }
        public DbSet<User> Users { get; set; }
    }
}