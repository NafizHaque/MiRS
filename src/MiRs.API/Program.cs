using Asp.Versioning;
using Flurl.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MiRs.DataAccess;
using MiRs.Domain.Mappers;
using MiRs.Interactors;
using MiRs.RunescapeClient;
using MiRs.DiscordClient;
using MiRs.Utils.Helpers;
using MiRS.Gateway.DataAccess;
using MiRS.Gateway.DiscordBotClient;
using MiRS.Gateway.RunescapeClient;
using System.Reflection;
using MiRs.Helpers;

namespace MiRs.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<RuneHunterDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddApiVersioning(
                options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;

                }).AddApiExplorer(options =>
                {
                    options.SubstituteApiVersionInUrl = true;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MiRS",
                    Description = "Yet Another OSRS Experiance Tracker.",
                    Version = "v1.0",
                });

                string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                options.CustomSchemaIds(x => x.FullName);
            });

            FlurlHttp.Clients.WithDefaults(builder =>
                builder.UseSocketsHttpHandler(shh =>
                {
                    shh.SslOptions.RemoteCertificateValidationCallback = (_, _, _, _) => { return true; };
                })
            );

            builder.Services.AddSingleton<IJsonSeraliserDefaultOptions, JsonSeraliserDefaultOptions>();

            builder.Services.AddScoped<GameMapper>();

            builder.Services.AddScoped<UserMapper>();

            builder.Services.AddScoped<IRuneClient, WOMClient>();

            builder.Services.AddScoped<IDiscordBotClient, DiscordBotClient>();

            builder.Services.AddScoped(typeof(IGenericSQLRepository<>), typeof(GenericSQLRepository<>));

            builder.Services.AddMediatRContracts();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("DevCors", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            WebApplication app = builder.Build();

            ApplyMigrations(app);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiRS V1");
                c.RoutePrefix = "swagger";
            });

            app.UseHttpsRedirection();

            app.UseCors("DevCors");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        static void ApplyMigrations(WebApplication app)
        {
            using (IServiceScope scope = app.Services.CreateScope())
            {
                RuneHunterDbContext runeHunterDbContext = scope.ServiceProvider.GetRequiredService<RuneHunterDbContext>();

                // Check and apply pending migrations
                IEnumerable<string> pendingMigrations = runeHunterDbContext.Database.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                    runeHunterDbContext.Database.Migrate();
                }
            }
        }
    }
}
