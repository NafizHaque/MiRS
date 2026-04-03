using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;
using MiRs.Domain.Logging;
using MiRs.Interactors.RuneHunter.Game;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.User;
using MiRS.Gateway.DataAccess;

namespace MiRs.Interactors.RuneHunter.User
{
    public class GetLatestUserLootInteractor : RequestHandler<GetLatestUserLootRequest, GetLatestUserLootResponse>
    {
        private readonly IGenericSQLRepository<RHUserRawLoot> _rhUserRawLoot;
        private readonly IGenericSQLRepository<RHUser> _rhUser;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGuildTeamInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public GetLatestUserLootInteractor(
            ILogger<LogUserLootInteractor> logger,
            IGenericSQLRepository<RHUserRawLoot> rhUserRawLoot,
            IGenericSQLRepository<RHUser> rhUserRepository,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _rhUserRawLoot = rhUserRawLoot;
            _rhUser = rhUserRepository;
            _appSettings = appSettings.Value;
        }

        // <summary>
        /// Handles the request to get recent user loot.
        /// </summary>
        /// <param name="request">The request to create the user raw loot.</param>
        /// <param name="result"></param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        protected override async Task<GetLatestUserLootResponse> HandleRequest(GetLatestUserLootRequest request, GetLatestUserLootResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.GameLogLoot, "Getting User Loot.");

            RHUser? usersInTable = (await _rhUser.Query(u =>
                u.UserId == request.UserId))
                .FirstOrDefault();

            if (usersInTable == null)
            {
                throw new BadRequestException($"User is not registered!");
            }

            IList<RHUserRawLoot> userLoot = (await _rhUserRawLoot.Query(l => l.UserId == usersInTable.UserId)).OrderByDescending(l => l.DateLogged).Take(6).ToList();

            result.Loots = userLoot;

            return result;
        }
    }
}
