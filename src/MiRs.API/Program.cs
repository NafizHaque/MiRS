using Asp.Versioning;
using Flurl.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MiRs.DataAccess;
using MiRs.DiscordClient;
using MiRs.Domain.Mappers;
using MiRs.Helpers;
using MiRs.Interactors;
using MiRs.RunescapeClient;
using MiRs.Utils.Helpers;
using MiRS.Gateway.DataAccess;
using MiRS.Gateway.DiscordBotClient;
using MiRS.Gateway.RunescapeClient;
using System.Reflection;

namespace MiRs.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            IConfigurationSection azureAd = builder.Configuration.GetSection("AzureAd");
            string azure_tenant_id = azureAd["TenantId"];
            string azure_client_id = azureAd["ClientId"];
            string azure_authority = azureAd["Authority"];



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

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"https://login.microsoftonline.com/{azure_tenant_id}/oauth2/v2.0/authorize"),
                            TokenUrl = new Uri($"https://login.microsoftonline.com/{azure_tenant_id}/oauth2/v2.0/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { $"api://{azure_client_id}/Admin.Access", "Access API as admin user" }
                            }
                        }
                    }
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" } },
                        new[] { $"api://{azure_client_id}/Admin.Access" }
                    }
                });

                string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                options.CustomSchemaIds(x => x.FullName);
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = azure_authority;
                    options.Audience = azure_client_id;
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

            builder.Services.AddAuthorization();

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

                // Enable OAuth2 login from Swagger
                c.OAuthClientId($"{azure_client_id}");
                c.OAuthAppName("My API Swagger");
                c.OAuthUsePkce();
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
