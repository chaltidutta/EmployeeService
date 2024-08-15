using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OrderService.Models;
using OrderService.Services;
using Serilog;
namespace OrderService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"], // Replace with builder.Configuration["Jwt:Issuer"] in production.
                        ValidAudience = builder.Configuration["Jwt:Audience"], // Replace with builder.Configuration["Jwt:Audience"] in production.
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                    options.TokenValidationParameters.RoleClaimType = "Role"; // Set role claim type for JWT.
                });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IOrderService,OrderService.Services.OrderService>();

            //Adding CORS
            builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
            // Add logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();


            //Logging functionality
            var logger = new LoggerConfiguration()
                .WriteTo
                .File("C:/Users/pulkit/source/repos/logs/orderlog.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            builder.Services.AddSerilog(logger);
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            //Using Cors
            app.UseCors();

            app.MapControllers();

            app.Run();
        }
    }
}
