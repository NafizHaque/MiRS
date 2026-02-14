using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.Discord;
using MiRs.Domain.Logging;
using MiRs.Interactors.RuneHunter.Admin.Event;
using MiRs.Mediator;
using MiRs.Mediator.Models.Discord;
using MiRS.Gateway.DataAccess;

namespace MiRs.Interactors.Discord
{
    public class GuildPermissionsInteractor : RequestHandler<GuildPermissionsRequest, GuildPermissionsResponse>
    {
        private readonly IGenericSQLRepository<GuildPermissions> _perms;

        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuildPermissionsInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public GuildPermissionsInteractor(
            ILogger<UpdateEventVerificationInteractor> logger,
            IGenericSQLRepository<GuildPermissions> perms,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _perms = perms;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to create a Guild team.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<GuildPermissionsResponse> HandleRequest(GuildPermissionsRequest request, GuildPermissionsResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.UpdateEventVerify, "Getting guild perms for: {guildId} ", request.GuildId);

            result.GuildPermissions = (await _perms.Query(gp => gp.GuildId == request.GuildId && gp.Type == request.permissionType)).FirstOrDefault();

            return result;
        }
    }
}
