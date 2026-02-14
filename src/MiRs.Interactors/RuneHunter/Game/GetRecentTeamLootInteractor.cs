using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.Discord;
using MiRs.Domain.Entities.Discord.Enums;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;
using MiRs.Domain.Logging;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.Game;
using MiRS.Gateway.DataAccess;

namespace MiRs.Interactors.RuneHunter.Game
{
    public class GetRecentTeamLootInteractor : RequestHandler<GetRecentTeamLootRequest, GetRecentTeamLootResponse>
    {
        private readonly IGenericSQLRepository<GuildEvent> _guildevent;
        private readonly IGenericSQLRepository<RHUserToTeam> _userToTeam;
        private readonly IGenericSQLRepository<RHUserRawLoot> _rhUserRawLoot;
        private readonly IGenericSQLRepository<GuildPermissions> _perms;

        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRecentTeamLootInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public GetRecentTeamLootInteractor(
            ILogger<ProcessUserLootInteractor> logger,
            IGenericSQLRepository<GuildEvent> guildevent,
            IGenericSQLRepository<RHUserToTeam> userToTeam,
            IGenericSQLRepository<RHUserRawLoot> rhUserRawLoot,
            IGenericSQLRepository<GuildPermissions> perms,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _userToTeam = userToTeam;
            _guildevent = guildevent;
            _rhUserRawLoot = rhUserRawLoot;
            _perms = perms;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to update game state.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<GetRecentTeamLootResponse> HandleRequest(GetRecentTeamLootRequest request, GetRecentTeamLootResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.GameGetMetadata, "Get Teams Loot for current guild event by User Id and Guild Id.");

            GuildEvent activeGuildEvent = (await _guildevent.GetAllEntitiesAsync(ge => ge.GuildId == request.GuildId && ge.EventActive == true, default,
                                                                                    ge => ge.Include(et => et.EventTeams)
                                                                                            .ThenInclude(t => t.Team)
                                                                                            .ThenInclude(ut => ut.UsersInTeam))).FirstOrDefault();

            IList<RHUserToTeam> userTeams = (await _userToTeam.Query(ge => ge.UserId == request.UserId)).ToList();
            GuildEventTeam? eventTeam = activeGuildEvent.EventTeams.FirstOrDefault(et => userTeams.Any(ut => ut.TeamId == et.TeamId));

            if (eventTeam == null)
            {
                throw new BadRequestException("Team Associated with Event cannot be found!");
            }

            HashSet<ulong> userIds = eventTeam.Team.UsersInTeam.Select(u => u.UserId).ToHashSet();

            result.Loots = (await _rhUserRawLoot.Query(ul => userIds.Contains(ul.UserId) && ul.Processed)).OrderByDescending(l => l.DateLogged).Take(50).ToList();

            result.TeamName = eventTeam.Team.TeamName;

            //Move into its own method later
            GuildPermissions guildPermissions = (await _perms.Query(gp => (gp.TeamId == eventTeam.Team.Id && gp.Type == PermissionType.Team) || gp.MessageId == request.MessageId)).FirstOrDefault();

            if (guildPermissions is null && request.ChannelId.HasValue && request.MessageId.HasValue)
            {
                await _perms.AddAsync(new GuildPermissions
                {
                    GuildId = request.GuildId,
                    ChannelId = (ulong)request.ChannelId,
                    Type = PermissionType.Team,
                    MessageId = request.MessageId,
                    TeamId = eventTeam.Team.Id,
                });
            }

            return result;
        }
    }
}
