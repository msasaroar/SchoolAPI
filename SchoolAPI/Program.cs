using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data;
using System.Text.Json.Serialization;

namespace SchoolAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // PostgreSQL Connection
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add Controllers with JSON Options to Ignore Cycles
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            // Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Response Compression
            builder.Services.AddResponseCompression();

            // Shows UseCors with CorsPolicyBuilder.
            builder.Services.AddCors(options =>
                                         options.AddPolicy("CorsPolicy", builder =>
                                         {
                                             builder.AllowAnyOrigin()
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod()
                                                    .SetIsOriginAllowed((host) => true)
                                                    .AllowCredentials();

                                             builder.WithOrigins("https://localhost:4200")
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod()
                                                    .SetIsOriginAllowed((host) => true)
                                                    .AllowCredentials();
                                         })
                                    );

            var app = builder.Build();

            // Developer Exception Page & Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            // Exclude Swagger from Response Compression
            app.UseWhen(context => !context.Request.Path.StartsWithSegments("/swagger"), appBuilder =>
            {
                appBuilder.UseResponseCompression();
            });

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}


/*
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data;

namespace SchoolAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            // Optional: Add response compression
            builder.Services.AddResponseCompression();

            // PostgreSQL Connection
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Enable Developer Exception Page explicitly
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.MapOpenApi();
                app.MapSwagger();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // ? Use response compression EXCEPT for /swagger to prevent Content-Length mismatch
            app.UseWhen(context => !context.Request.Path.StartsWithSegments("/swagger"), appBuilder =>
            {
                appBuilder.UseResponseCompression();
            });

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
*/