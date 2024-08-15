using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using TaskService.Models;
using TaskService.Services;

namespace TaskService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create a new builder for the web application.
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Configure JWT authentication.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // Set token validation parameters.
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "www.abc.com", // Replace with builder.Configuration["Jwt:Issuer"] in production.
                        ValidAudience = "www.abc.com", // Replace with builder.Configuration["Jwt:Audience"] in production.
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                    options.TokenValidationParameters.RoleClaimType = "Role"; // Set role claim type for JWT.
                });

            // Add controllers to the service container.
            builder.Services.AddControllers();

            // Add Swagger/OpenAPI services.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<TaskContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
            ServiceLifetime.Singleton);
            // Add scoped service for ITaskService and singleton service for TaskContext.
            builder.Services.AddScoped<ITaskService, TaskServices>();

            //Adding CORS
            builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            // Configure logging functionality with Serilog.
            var logger = new LoggerConfiguration()
                .WriteTo.File("C:/Users/pulkit/source/repos/logs/tasklog.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            builder.Services.AddSerilog(logger);

            // Build the application.
            var app = builder.Build();


            // Use Swagger and Swagger UI in development environment.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enable authentication and authorization middleware.
            app.UseAuthentication();
            app.UseAuthorization();

            //Using Cors
            app.UseCors();

            // Map controllers to endpoints.
            app.MapControllers();

            // Run the application.
            app.Run();
        }
    }
}
