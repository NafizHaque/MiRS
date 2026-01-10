using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Logging;
using MiRS.Gateway.DataAccess;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.Admin.Event;
using MiRs.Interactors.RuneHunter.Admin.Team;

namespace MiRs.Interactors.RuneHunter.Admin.Event
{
    public class GetGuildEventsInteractor : RequestHandler<GetGuildEventsRequest, GetGuildEventsResponse>
    {
        private readonly IGenericSQLRepository<GuildEvent> _guildEventRepository;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetGuildEventsInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildEventRepository">The repo interface to table storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public GetGuildEventsInteractor(
            ILogger<GetGuildTeamsInteractor> logger,
            IGenericSQLRepository<GuildEvent> guildEventRepository,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
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
        protected override async Task<GetGuildEventsResponse> HandleRequest(GetGuildEventsRequest request, GetGuildEventsResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.GetGuildEvent, "Retrieving Guild Events. Guild Id: {guildId}", request.GuildId);

            result.GuildEvents = await _guildEventRepository.Query(g => g.GuildId == request.GuildId);

            return result;
        }
    }
}
