using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Infrastructure.Entities;
using PMS.Infrastructure.Enums;

namespace PMS.Infrastructure.Data.EntityTypeConfiguration
{
    internal class ProjectEntityConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Tasks)
            .WithOne(x => x.Project)
            .HasForeignKey(x => x.ProjectId);

            builder.Property(p => p.Status)
            .HasConversion(
                s => s.ToString(),
                s => (ProjectStatus)Enum.Parse(typeof(ProjectStatus), s)
            );
        }
    }
}