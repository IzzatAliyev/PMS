using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Infrastructure.Entities;
using PMS.Infrastructure.Enums;

namespace PMS.Infrastructure.Data.EntityTypeConfiguration
{
    internal class EmployeeProjectEntityConfiguration : IEntityTypeConfiguration<EmployeeProject>
    {
        public void Configure(EntityTypeBuilder<EmployeeProject> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.HasOne(x => x.Employee)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Project)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.Task)
            .HasConversion(
                t => t.ToString(),
                t => (EmployeeTask)Enum.Parse(typeof(EmployeeTask), t)
            );
        }
    }
}