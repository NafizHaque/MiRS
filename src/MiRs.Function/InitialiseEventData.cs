using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MiRs.Function;

public class InitialiseEventData
{
    private readonly ILogger _logger;

    public InitialiseEventData(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<InitialiseEventData>();
    }

    [Function("InitialiseEventData")]
    public void Run([TimerTrigger("0,30 * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.Now);

        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
        }
    }
}