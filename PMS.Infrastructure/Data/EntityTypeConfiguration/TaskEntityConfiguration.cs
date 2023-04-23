using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Infrastructure.Entities;
using PMS.Infrastructure.Enums;

namespace PMS.Infrastructure.Data.EntityTypeConfiguration
{
    internal class TaskEntityConfiguration : IEntityTypeConfiguration<PTask>
    {
        public void Configure(EntityTypeBuilder<PTask> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.AssignedTo)
            .WithMany(x => x.TasksFrom)
            .HasForeignKey(x => x.AssignedToId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.AssignedFrom)
            .WithMany(x => x.TasksTo)
            .HasForeignKey(x => x.AssignedFromId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Project)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Property(t => t.TaskType)
            .HasConversion(
                t => t.ToString(),
                t => (PTaskType)Enum.Parse(typeof(PTaskType), t)
            );

            builder.Property(t => t.Status)
            .HasConversion(
                s => s.ToString(),
                s => (PTaskStatus)Enum.Parse(typeof(PTaskStatus), s)
            );
        }
    }
}