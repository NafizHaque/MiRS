using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.DTOs.Discord;
using MiRs.Domain.Entities.Discord;
using MiRs.Domain.Entities.Discord.Enums;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;
using MiRs.Domain.Logging;
using MiRs.Interactors.RuneHunter.Admin.Team;
using MiRs.Mediator;
using MiRs.Mediator.Models.Discord;
using MiRS.Gateway.DataAccess;
using MiRS.Gateway.DiscordBotClient;

namespace MiRs.Interactors.Discord
{
    public class LatestTeamLootAlertInteractor : RequestHandler<LatestTeamLootAlertRequest, LatestTeamLootAlertResponse>
    {
        private readonly AppSettings _appSettings;
        private readonly ISender _mediator;
        private readonly IDiscordBotClient _discordBotClient;
        private readonly IGenericSQLRepository<GuildTeam> _guildTeamRepository;
        private readonly IGenericSQLRepository<GuildPermissions> _perms;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGuildTeamInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public LatestTeamLootAlertInteractor(
            ILogger<CreateGuildTeamInteractor> logger,
            IGenericSQLRepository<GuildTeam> guildTeamRepository,
            IDiscordBotClient discordBotClient,
            ISender mediator,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _guildTeamRepository = guildTeamRepository;
            _appSettings = appSettings.Value;
            _discordBotClient = discordBotClient;
            _mediator = mediator;

        }

        /// <summary>
        /// Handles the request to Loot Alert.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<LatestTeamLootAlertResponse> HandleRequest(LatestTeamLootAlertRequest request, LatestTeamLootAlertResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.CreateGuildTeam, "Creating Loot Alert.");

            IList<GuildPermissions> TeamsAndPerms = (await _perms.Query(gp => gp.Type == PermissionType.Team)).ToList();

            foreach (GuildPermissions teamPerms in TeamsAndPerms)
            {
                GuildTeam guildTeam = (await _guildTeamRepository.GetAllEntitiesAsync(g => g.Id == teamPerms.TeamId, default, gt => gt.Include(gt => gt.UsersInTeam))).FirstOrDefault();

                if (guildTeam == null)
                {
                    throw new BadRequestException("Could not find team to send alert for!");
                }

                await _discordBotClient.LatestTeamLootAlert(new LootAlertDto
                {
                    UserId = guildTeam.UsersInTeam.FirstOrDefault().UserId,
                    GuildId = guildTeam.GuildId,
                    ResponseId = teamPerms.ChannelId,
                    ResponseToken = teamPerms.ResponseToken,
                });
            }

            return result;
        }
    }
}
