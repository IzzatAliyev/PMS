using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Infrastructure.Entities;

namespace PMS.Infrastructure.Data.EntityTypeConfiguration
{
    internal class ProjectRoleEntityConfiguration : IEntityTypeConfiguration<ProjectRole>
    {
        public void Configure(EntityTypeBuilder<ProjectRole> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Project)
            .WithOne(x => x.Role)
            .HasForeignKey<ProjectRole>(x => x.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}