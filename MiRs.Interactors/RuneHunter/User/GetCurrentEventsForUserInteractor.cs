using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Logging;
using MiRS.Gateway.DataAccess;
using MiRs.Mediator.Models.RuneHunter.Admin;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.User;

namespace MiRs.Interactors.RuneHunter.User
{
    public class GetCurrentEventsForUserInteractor : RequestHandler<GetCurrentEventsForUserRequest, GetCurrentEventsForUserResponse>
    {
        private readonly IGenericSQLRepository<RHUser> _userRepository;
        private readonly IGenericSQLRepository<GuildEvent> _guildEventRepository;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGuildTeamInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public GetCurrentEventsForUserInteractor(
            ILogger<GetCurrentEventsForUserInteractor> logger,
            IGenericSQLRepository<GuildEvent> guildEventRepository,
            IGenericSQLRepository<RHUser> userRepository,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _guildEventRepository = guildEventRepository;
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to create a Guild team.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<GetCurrentEventsForUserResponse> HandleRequest(GetCurrentEventsForUserRequest request, GetCurrentEventsForUserResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.CreateGuildTeam, "Return current Guild Events for User. User Id: {userId}, Guild Id: {guildId}", request.UserId, request.GuildId);

            RHUser? user = (await _userRepository.GetAllEntitiesAsync(u => u.UserId == request.UserId, default, utt => utt.UserToTeams)).FirstOrDefault();

            return result;
        }
    }
}
