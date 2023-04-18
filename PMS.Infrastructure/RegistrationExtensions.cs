using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMS.Infrastructure.Interfaces;
using PMS.Infrastructure.Data;

namespace PMS.Infrastructure
{
    public static class RegistrationExtensions
    {
        public static void AddStorage(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<PMSDbContext>(options =>
            {
                options.UseMySQL(configuration["ConnectionStrings:DefaultConnection"]);
            });

            serviceCollection.AddDbContext<PMSIdentityDbContext>(options =>
            {
                options.UseMySQL(configuration["ConnectionStrings:IdentityConnection"]);
            });

            serviceCollection.AddScoped<IPMSDbContext, PMSDbContext>();
            serviceCollection.AddScoped<PMSIdentityDbContext>();
        }
    }
}