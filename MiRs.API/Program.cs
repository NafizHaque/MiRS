using Asp.Versioning;
using Flurl.Http;
using Microsoft.OpenApi.Models;
using MiRs.Domain.Mappers;
using MiRs.Interactors;
using MiRs.Interfaces.Helpers;
using MiRs.RunescapeClient;
using MiRs.Utils.Helpers;
using MiRS.Gateway.RunescapeClient;
using System.Reflection;

namespace MiRs.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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

            builder.Services.AddScoped<UserMapper>();

            builder.Services.AddScoped<IRuneClient, WOMClient>();

            builder.Services.AddMediatRContracts();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
