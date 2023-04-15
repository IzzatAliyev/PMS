using PMS.Core.Repositories;
using PMS.Core.Repositories.Repositories;
using PMS.Infrastructure;
using PMS.Infrastructure.Data;
using PMS.Service.Services.Impl;
using PMS.Service.Services.Interfaces;

namespace PMS.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddStorage(builder.Configuration);
            builder.Services.AddMemoryCache();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<ISkillService, SkillService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<IEmployeeProjectService, EmployeeProjectService>();
            builder.Services.AddScoped<IEmployeeRoleService, EmployeeRoleService>();
            builder.Services.AddScoped<IEmployeeSkillService, EmployeeSkillService>();
            builder.Services.AddScoped<IProjectRoleService, ProjectRoleService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            await app.DatabaseEnsureCreated();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
