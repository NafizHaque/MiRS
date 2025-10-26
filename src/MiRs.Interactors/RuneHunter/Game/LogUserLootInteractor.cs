using MiRs.Mediator.Models.RuneHunter.Admin;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.Game;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRS.Gateway.DataAccess;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Interactors.RuneHunter.Admin;
using MiRs.Domain.Logging;
using System.Text.RegularExpressions;
using MiRs.Domain.Exceptions;

namespace MiRs.Interactors.RuneHunter.Game
{
    public class LogUserLootInteractor : RequestHandler<LogUserLootRequest, LogUserLootResponse>
    {
        private readonly IGenericSQLRepository<RHUserRawLoot> _rhUserRawLoot;
        private readonly IGenericSQLRepository<RHUser> _rhUserRepository;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGuildTeamInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public LogUserLootInteractor(
            ILogger<LogUserLootInteractor> logger,
            IGenericSQLRepository<RHUserRawLoot> rhUserRawLoot,
            IGenericSQLRepository<RHUser> rhUserRepository,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _rhUserRawLoot = rhUserRawLoot;
            _rhUserRepository = rhUserRepository;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to create a Guild team.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<LogUserLootResponse> HandleRequest(LogUserLootRequest request, LogUserLootResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.GameLogLoot, "Logging User Loot.");

            string pattern = @"\*\*(.*?)\*\* - Just got \*\*(?:(\d+)x\s+)?(.+?)\*\* from (?:lvl (\d+) )?\*\*(.*?)\*\*";

            Match match = Regex.Match(request.LootMessage, pattern);

            if (!match.Success)
            {
                throw new BadRequestException($"Loot message is malformed! Does not follow Regex pattern!");
            }

            string UsernameFromLootLogged = match.Groups[1].Value;

            RHUserRawLoot userLoot = new RHUserRawLoot
            {
                Username = match.Groups[1].Value,
                Quantity = string.IsNullOrEmpty(match.Groups[2].Value) ? 1 : int.Parse(match.Groups[2].Value),
                Loot = match.Groups[3].Value,
                Mobname = match.Groups[5].Value,
            };

            RHUser usersInTable = (await _rhUserRepository.Query(u =>
                u.Runescapename.ToLower() == UsernameFromLootLogged.ToLower()))
                .FirstOrDefault();

            if (usersInTable == null)
            {
                throw new BadRequestException($"User is not registered to log drops!");
            }

            userLoot.UserId = usersInTable.UserId;

            userLoot.DateLogged = DateTimeOffset.UtcNow;

            await _rhUserRawLoot.AddAsync(userLoot);

            return result;
        }
    }
}
