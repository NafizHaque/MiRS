using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Mediator.Models.RuneHunter.Game;
using MiRS.Gateway.DataAccess;

namespace MiRs.Function
{
    public class InitialiseEventData
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<ProcessGameState> _logger;
        private readonly ISender _mediator;
        private readonly IGenericSQLRepository<GuildEvent> _guildEventRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessGameState"/> class.
        /// </summary>
        /// <param name="appSettings">App settings for the application.</param>
        /// <param name="logger">The logging interface.</param>
        /// <param name="mediator">The mediator service.</param>
        public InitialiseEventData(
            IOptions<AppSettings> appSettings,
            ILogger<ProcessGameState> logger,
            IGenericSQLRepository<GuildEvent> guildEventRepository,
            ISender mediator)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
            _mediator = mediator;
            _guildEventRepository = guildEventRepository;
        }

        [Function("InitialiseEventData")]
        public async Task RunAsync([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation("InitialiseEventData function executed at: {executionTime}", DateTime.Now);

            try
            {
                IEnumerable<GuildEvent> gameEvents = (await _guildEventRepository.GetAllEntitiesAsync(ge => ge.EventStart >= DateTimeOffset.UtcNow.AddMinutes(-5) && ge.EventStart <= DateTimeOffset.UtcNow && !ge.EventActive, default, ge => ge.Include(ge => ge.EventTeams))).ToList();

                if (!gameEvents.Any())
                {
                    return;
                }

                foreach (GuildEvent gameEvent in gameEvents)
                {
                    if (!gameEvent.EventTeams.Any())
                    {
                        continue;
                    }

                    gameEvent.EventActive = true;

                    foreach (GuildEventTeam geTeam in gameEvent.EventTeams)
                    {

                        await _mediator.Send(new InitaliseTeamProgressRequest { EventId = geTeam.EventId, TeamId = geTeam.TeamId });
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured");

            }

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation("InitialiseEventData function executed at: {executionTime}", DateTime.Now);
                _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
            }
        }
    }
}