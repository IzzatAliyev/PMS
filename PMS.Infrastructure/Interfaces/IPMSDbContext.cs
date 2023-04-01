using Microsoft.EntityFrameworkCore;
using PMS.Infrastructure.Entities;

namespace PMS.Infrastructure.Interfaces
{
    public interface IPMSDbContext: IDisposable
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<EmployeeProject> EmployeeProjects { get; set; }
        DbSet<EmployeeRole> EmployeeRoles { get; set; }
        DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<ProjectRole> ProjectRoles { get; set; }
        DbSet<Skill> Skills { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}