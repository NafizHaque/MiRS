using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Logging;
using MiRS.Gateway.DataAccess;
using MiRs.Mediator.Models.RuneHunter.Admin;
using MiRs.Mediator;

namespace MiRs.Interactors.RuneHunter.Admin
{
    public class CreateGuildEventInteractor : RequestHandler<CreateEventInGuildRequest, CreateEventInGuildResponse>
    {
        private readonly IGenericSQLRepository<GuildEvent> _guildEventRepository;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGuildTeamInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public CreateGuildEventInteractor(
            ILogger<CreateGuildEventInteractor> logger,
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
        protected override async Task<CreateEventInGuildResponse> HandleRequest(CreateEventInGuildRequest request, CreateEventInGuildResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.CreateGuildTeam, "Creating Guild Event. Guild Id: {guildId}, EventName: {teamname} ", request.GuildEventToBeCreated.GuildId, request.GuildEventToBeCreated.Eventname);

            request.GuildEventToBeCreated.CreatedDate = DateTimeOffset.UtcNow;

            await _guildEventRepository.AddAsync(request.GuildEventToBeCreated);

            return result;
        }
    }
}
