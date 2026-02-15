using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.DTOs.RuneHunter;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.Admin.Event;
using MiRS.Gateway.DataAccess;

namespace MiRs.Interactors.RuneHunter.Admin.Event
{
    public class GetAllEventsInteractor : RequestHandler<GetAllEventsRequest, GetAllEventsResponse>
    {
        private readonly IGenericSQLRepository<GuildEventTeam> _guildTeamEventRepository;
        private readonly IGenericSQLRepository<GuildTeam> _guildTeamRepository;
        private readonly IGenericSQLRepository<GuildEvent> _guildEventRepository;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllEventsInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public GetAllEventsInteractor(
            ILogger<AddGuildTeamToEventInteractor> logger,
            IGenericSQLRepository<GuildEventTeam> guildTeamEventRepository,
            IGenericSQLRepository<GuildTeam> guildTeamRepository,
            IGenericSQLRepository<GuildEvent> guildEventRepository,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _guildTeamEventRepository = guildTeamEventRepository;
            _guildTeamRepository = guildTeamRepository;
            _guildEventRepository = guildEventRepository;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to create a Guild team.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<GetAllEventsResponse> HandleRequest(GetAllEventsRequest request, GetAllEventsResponse result, CancellationToken cancellationToken)
        {
            IList<GuildEvent> gameEvents = (await _guildEventRepository.Query(g => true)).ToList();

            foreach (GuildEvent gameEvent in gameEvents)
            {
                IList<GuildEventTeam> teamsfromEvent = (await _guildTeamEventRepository.QueryWithInclude(e => e.EventId == gameEvent.Id, default, eg => eg.Include(egt => egt.Team).ThenInclude(utt => utt.UsersInTeam).ThenInclude(u => u.User))).ToList();

                int playerCount = teamsfromEvent
                    .Where(tfe => tfe.Team != null)
                    .Sum(tfe => tfe.Team.UsersInTeam.Count);

                result.GuildEvents.Add(new GameEvent
                {
                    Id = gameEvent.Id,
                    GuildId = gameEvent.GuildId,
                    Eventname = gameEvent.Eventname,
                    EventActive = gameEvent.EventActive,
                    CreatedDate = gameEvent.CreatedDate,
                    EventStart = gameEvent.EventStart,
                    EventEnd = gameEvent.EventEnd,
                    TotalPlayers = playerCount,
                });
            }

            return result;
        }
    }
}
