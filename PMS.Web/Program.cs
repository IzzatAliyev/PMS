using Microsoft.AspNetCore.Mvc.Razor;
using PMS.Core.Repositories;
using PMS.Core.Repositories.Repositories;
using PMS.Infrastructure;
using PMS.Infrastructure.Data;
using PMS.Service.Services.Impl;
using PMS.Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMS.Infrastructure.Entities;
using PMS.Web.Hubs;
using PMS.Service.Services;

namespace PMS.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("IdentityConnection") ?? throw new InvalidOperationException("Connection string 'IdentityConnection' not found.");

            builder.Services.AddDbContext<PMSIdentityDbContext>(options => options.UseMySQL(connectionString));

            builder.Services
            .AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<PMSIdentityDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/auth/login";
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSignalR();

            builder.Services.AddStorage(builder.Configuration);
            builder.Services.AddIdentityStorage(builder.Configuration);

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
            builder.Services.AddSingleton<ChatService>();


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

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.SetMinimumLevel(LogLevel.Warning);
            builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning); 

            var app = builder.Build();
            await app.DatabaseEnsureCreated();
            await app.IdentityDatabaseEnsureCreated();

            // await DummyDataGenerator.GenerateAsync(app.Services); 

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/SomethingWentWrong");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // try
            // {
            //     var chatService = app.Services.GetRequiredService<ChatService>();

            //     var message = new Message
            //     {
            //         SenderId = 1,
            //         RecipientId = 2,
            //         Content = "hsadas",
            //         CreatedAt = DateTime.Now,
            //         UpdatedAt  = DateTime.Now
            //     };

            //     // Use the chatService to interact with the MongoDB database.
            //     // For example:
            //     await chatService.Create(message);
            //     System.Console.WriteLine("created");
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine("An error occurred while seeding the database: " + ex.Message);
            // }

            app.UseDeveloperExceptionPage();
            // app.UseHttpsRedirection();
            app.UseRequestLocalization();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapHub<ChatHub>("/chathub");
            app.MapHub<NotificationHub>("/notificationhub");

            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            // app.MapFallbackToController("NotFound", "Home");

            app.Run();
        }
    }
}
