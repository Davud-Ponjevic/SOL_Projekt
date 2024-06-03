using Backend.AppDBContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((hostContext, services) =>
                    {
                        services.AddControllers();

                        // Swagger-Dienste hinzuf�gen
                        services.AddSwaggerGen(c =>
                        {
                            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend", Version = "v1" });
                        });

                        // EndpointsApiExplorer-Dienste hinzuf�gen
                        services.AddEndpointsApiExplorer();

                        // Konfiguration laden
                        var configuration = new ConfigurationBuilder()
                            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                            .AddJsonFile("appsettings.json")
                            .Build();

                        // Verbindungszeichenfolge aus der Konfiguration lesen
                        var connectionString = configuration.GetConnectionString("DefaultConnection");

                        // AppDbContext mit Verbindungszeichenfolge registrieren
                        services.AddDbContext<AppDbContext>(options =>
                        {
                            options.UseSqlServer(connectionString);
                        });

                        // Weitere Dienste registrieren...

                        // CORS-Konfiguration hinzuf�gen
                        var allowedOrigins = configuration["CORS:AllowedOrigins"]?.Split(',') ?? new string[0];
                        services.AddCors(options =>
                        {
                            options.AddPolicy("CorsPolicy", policy =>
                            {
                                policy.WithOrigins(allowedOrigins)
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                            });
                        });
                    });

                    webBuilder.Configure(app =>
                    {
                        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

                        if (env.IsDevelopment())
                        {
                            // Middleware f�r Swagger aktivieren
                            app.UseSwagger();
                            app.UseSwaggerUI(c =>
                            {
                                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend");
                            });
                        }

                        app.UseRouting();
                        app.UseAuthorization();

                        // UseCors Middleware hinzuf�gen
                        app.UseCors("CorsPolicy");

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                        });
                    });
                });
    }
}
