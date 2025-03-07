using System.Diagnostics;
using System.Reflection;
using Asp.Versioning;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MiRs.Interactors;
using MiRS.Gateway.RunescapeClient;
using MiRs.RunescapeClient;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MiRs.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            builder.Services.AddScoped<IRuneClient, WOMClient>();

            builder.Services.AddMediatRContracts();

            var app = builder.Build();

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
