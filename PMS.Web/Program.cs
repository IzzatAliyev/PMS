using Microsoft.AspNetCore.Mvc.Razor;
using PMS.Core.Repositories;
using PMS.Core.Repositories.Repositories;
using PMS.Infrastructure;
using PMS.Infrastructure.Data;
using PMS.Service.Services.Impl;
using PMS.Service.Services.Interfaces;

namespace PMS.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddStorage(builder.Configuration);

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<ISkillService, SkillService>();
            builder.Services.AddScoped<IProjectTaskService, ProjectTaskService>();
            builder.Services.AddScoped<IEmployeeProjectService, EmployeeProjectService>();
            builder.Services.AddScoped<IEmployeeRoleService, EmployeeRoleService>();
            builder.Services.AddScoped<IEmployeeSkillService, EmployeeSkillService>();
            builder.Services.AddScoped<IProjectRoleService, ProjectRoleService>();

            builder.Services.AddLocalization(o => { o.ResourcesPath = "Resources"; });
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture("en");
                options.AddSupportedUICultures("en", "uk", "de");
                options.AddSupportedCultures("en", "uk", "de");
                options.FallBackToParentUICultures = false;
            });

            builder.Services.AddControllersWithViews()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();

            var app = builder.Build();
            await app.DatabaseEnsureCreated();
            
            // await DummyDataGenerator.GenerateAsync(app.Services);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseRequestLocalization();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            
            app.MapFallbackToController("NotFound", "Home");

            app.Run();
        }
    }
}
