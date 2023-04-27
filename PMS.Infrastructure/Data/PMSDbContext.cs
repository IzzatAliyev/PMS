using Microsoft.EntityFrameworkCore;
using PMS.Infrastructure.Interfaces;
using PMS.Infrastructure.Entities;
using PMS.Infrastructure.Enums;

namespace PMS.Infrastructure.Data
{
    public class PMSDbContext : DbContext, IPMSDbContext
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
            modelBuilder.SetBaseEntityProperties();
            
            modelBuilder.Ignore<User>();

            var admin = new Employee
            {
                Id = 1,
                UserName = "Admin",
                FirstName = "Admin",
                LastName = "Adminov",
                Email = "admin@gmail.com",
                Description = "Hi There!!",
                Position = EmployeePosition.CEO,
                PhoneNumber = "111",
                ProfilePicture = ""
            };
            var employee = new Employee
            {
                Id = 2,
                UserName = "Employee",
                FirstName = "Employee",
                LastName = "Employov",
                Email = "employee@gmail.com",
                Description = "Hi There!!",
                Position = EmployeePosition.JuniorDeveloper,
                PhoneNumber = "222",
                ProfilePicture = ""
            };

            modelBuilder.Entity<Employee>().HasData(admin, employee);
        }
    }
}