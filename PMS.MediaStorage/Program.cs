using PMS.MediaStorage.Models;

namespace PMS.MediaStorage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<MediaStorageViewModel>(provider =>
            {
                var env = provider.GetService<IWebHostEnvironment>();
                var mediaFolder = Path.Combine(env.WebRootPath, "media");
                return new MediaStorageViewModel(mediaFolder);
            }); ;

            System.IO.Directory.CreateDirectory("wwwroot/media");
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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