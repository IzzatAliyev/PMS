using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Infrastructure.Entities;

namespace PMS.Infrastructure.Data.EntityTypeConfiguration
{
    internal class EmployeeEntityConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Tasks)
            .WithOne(x => x.Employee)
            .HasForeignKey(x => x.EmployeeId);
        }
    }
}