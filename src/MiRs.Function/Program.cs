using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

FunctionsApplicationBuilder builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services.AddDbContext<RuneHunterDbContext>(options =>
    options.UseSqlServer(builder.Configuration["DefaultConnection"]));

builder.Services.AddSingleton<IJsonSeraliserDefaultOptions, JsonSeraliserDefaultOptions>();

builder.Services.AddScoped<UserMapper>();

builder.Services.AddScoped<GameMapper>();

builder.Services.AddScoped<IRuneClient, WOMClient>();

builder.Services.AddScoped<IDiscordBotClient, DiscordBotClient>();

builder.Services.AddMediatRContracts();

builder.Services.AddScoped(typeof(IGenericSQLRepository<>), typeof(GenericSQLRepository<>));

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
