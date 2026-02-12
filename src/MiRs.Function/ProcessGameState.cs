using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Mediator.Models.RuneHunter.Game;

namespace MiRs.Function
{
    public class ProcessGameState
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<ProcessGameState> _logger;
        private readonly ISender _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessGameState"/> class.
        /// </summary>
        /// <param name="appSettings">App settings for the application.</param>
        /// <param name="userRawLoot">Interface to expose Db calls.</param>
        /// <param name="logger">The logging interface.</param>
        /// <param name="mediator">The mediator service.</param>
        public ProcessGameState(
            IOptions<AppSettings> appSettings,
            ILogger<ProcessGameState> logger,
            ISender mediator)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
            _mediator = mediator;
        }

        [Function("UpdateGameState")]
        public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation("UpdateGameState function executed at: {executionTime}", DateTimeOffset.UtcNow);

            try
            {
                await _mediator.Send(new ProcessUserLootRequest());

                await _mediator.Send(new UpdateGameStateRequest());

                await _mediator.Send(new UpdateEventWinnersRequest());

                _logger.LogInformation("UpdateGameState function completed at: {executionTime}", DateTimeOffset.UtcNow);
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
