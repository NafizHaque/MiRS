using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRS.Gateway.DataAccess;

namespace MiRs.Function;

public class ProcessUserLoot
{
    private readonly AppSettings _appSettings;
    private readonly IGenericSQLRepository<RHUserRawLoot> _userRawLoot;
    private readonly ILogger<ProcessUserLoot> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessUserLoot"/> class.
    /// </summary>
    /// <param name="appSettings">App settings for the application.</param>
    /// <param name="userRawLoot">Interface to expose Db calls.</param>
    /// <param name="logger">The logging interface.</param>
    public ProcessUserLoot(
        IOptions<AppSettings> appSettings,
        IGenericSQLRepository<RHUserRawLoot> userRawLoot,
        ILogger<ProcessUserLoot> logger)
    {
        _appSettings = appSettings.Value;
        _userRawLoot = userRawLoot;
        _logger = logger;
    }

    [Function("ProcessUserLoot")]
    public void Run([TimerTrigger("*/5 * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.Now);


        
        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
        }
    }
}