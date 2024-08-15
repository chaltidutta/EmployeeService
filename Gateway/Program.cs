using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Load configuration from ocelot.json
            builder.Configuration.AddJsonFile("ocelot.json");

            // Add Ocelot services
            builder.Services.AddOcelot(builder.Configuration);

            var app = builder.Build();

            app.UseHttpsRedirection();

            // Add Ocelot middleware
            app.UseOcelot().Wait();

            app.Run();
        }
    }
}
