using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Infrastructure.Entities;
using PMS.Infrastructure.Enums;

namespace PMS.Infrastructure.Data.EntityTypeConfiguration
{
    internal class EmployeeSkillEntityConfiguration : IEntityTypeConfiguration<EmployeeSkill>
    {
        public void Configure(EntityTypeBuilder<EmployeeSkill> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Employee)
            .WithMany(x => x.Skills)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Skill)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.SkillId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.Level)
            .HasConversion(
                l => l.ToString(),
                l => (SkillLevel)Enum.Parse(typeof(SkillLevel), l)
            );
        }
    }
}