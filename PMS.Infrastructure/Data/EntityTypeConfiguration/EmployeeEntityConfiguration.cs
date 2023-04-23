using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Infrastructure.Entities;
using PMS.Infrastructure.Enums;

namespace PMS.Infrastructure.Data.EntityTypeConfiguration
{
    internal class EmployeeEntityConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.TasksTo)
            .WithOne(x => x.AssignedFrom)
            .HasForeignKey(x => x.AssignedFromId);

            builder.HasMany(x => x.TasksFrom)
            .WithOne(x => x.AssignedTo)
            .HasForeignKey(x => x.AssignedToId);

            builder.Property(e => e.Position)
            .HasConversion(
                p => p.ToString(),
                p => (EmployeePosition)Enum.Parse(typeof(EmployeePosition), p)
            );
        }
    }
}