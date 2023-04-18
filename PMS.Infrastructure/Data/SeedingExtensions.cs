using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace PMS.Infrastructure.Data
{
    public static class SeedingExtensions
    {
        public static async Task DatabaseEnsureCreated(this IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PMSDbContext>();
                var identityDbContext = scope.ServiceProvider.GetRequiredService<PMSIdentityDbContext>();
                var database = dbContext.Database;
                var identityDatabase = identityDbContext.Database;
                await database.EnsureCreatedAsync();
                await identityDatabase.EnsureCreatedAsync();
            }
        }
    }
}