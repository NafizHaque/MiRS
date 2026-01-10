using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Logging;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.Admin.Team;
using MiRS.Gateway.DataAccess;

namespace MiRs.Interactors.RuneHunter.Admin.Team
{
    public class GetGuildTeamsInteractor : RequestHandler<GetGuildTeamsRequest, GetGuildTeamsResponse>
    {
        private readonly IGenericSQLRepository<GuildTeam> _guildTeamRepository;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to table storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public GetGuildTeamsInteractor(
            ILogger<GetGuildTeamsInteractor> logger,
            IGenericSQLRepository<GuildTeam> guildTeamRepository,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _guildTeamRepository = guildTeamRepository;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to create a Guild team.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<GetGuildTeamsResponse> HandleRequest(GetGuildTeamsRequest request, GetGuildTeamsResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.GetGuildTeam, "Retrieving Guild Team. Guild Id: {guildId}", request.GuildId);

            result.GuildTeams = await _guildTeamRepository.Query(g => g.GuildId == request.GuildId);

            return result;
        }
    }
}
