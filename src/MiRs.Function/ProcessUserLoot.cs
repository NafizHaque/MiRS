using System;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Logging;
using MiRS.Gateway.DataAccess;

namespace MiRs.Function
{
    public class ProcessUserLoot
    {
        private readonly AppSettings _appSettings;
        private readonly IGenericSQLRepository<RHUserRawLoot> _userRawLoot;
        private readonly ILogger<ProcessUserLoot> _logger;
        private readonly ISender _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessUserLoot"/> class.
        /// </summary>
        /// <param name="appSettings">App settings for the application.</param>
        /// <param name="userRawLoot">Interface to expose Db calls.</param>
        /// <param name="logger">The logging interface.</param>
        /// <param name="mediator">The mediator service.</param>
        public ProcessUserLoot(
            IOptions<AppSettings> appSettings,
            IGenericSQLRepository<RHUserRawLoot> userRawLoot,
            ILogger<ProcessUserLoot> logger,
            ISender mediator)
        {
            _appSettings = appSettings.Value;
            _userRawLoot = userRawLoot;
            _logger = logger;
            _mediator = mediator;
        }

        [Function("ProcessUserLoot")]
        public async Task Run([TimerTrigger("*/5 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTimeOffset.UtcNow);

            try
            {
                await _mediator.Send(new DeleteExpiredUserRequest());
            }
            catch (Exception ex)
            {
            }

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
            }
        }
    }
}
