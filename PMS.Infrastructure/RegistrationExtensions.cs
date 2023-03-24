using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMS.Core.Interfaces;
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

            serviceCollection.AddScoped<IPMSDbContext, PMSDbContext>();
        }
    }
}