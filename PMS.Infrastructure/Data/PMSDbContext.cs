using Microsoft.EntityFrameworkCore;
using PMS.Infrastructure.Interfaces;
using PMS.Infrastructure.Entities;

namespace PMS.Infrastructure.Data
{
    public class PMSDbContext: DbContext, IPMSDbContext
    {
        public PMSDbContext(DbContextOptions<PMSDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<PTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PMSDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}