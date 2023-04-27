using Microsoft.EntityFrameworkCore;
using PMS.Infrastructure.Entities;

namespace PMS.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void SetBaseEntityProperties(this ModelBuilder modelBuilder)
        {
            var baseEntityType = typeof(BaseEntity);
            var entityTypes = modelBuilder.Model.GetEntityTypes()
                .Where(e => baseEntityType.IsAssignableFrom(e.ClrType));

            foreach (var entityType in entityTypes)
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(BaseEntity.CreatedAt))
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAdd();

                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(BaseEntity.UpdatedAt))
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAdd();
                    // .ValueGeneratedOnAddOrUpdate(); // not working yet
            }
        }
    }
}