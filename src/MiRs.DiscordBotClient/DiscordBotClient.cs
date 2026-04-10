using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.DTOs.Discord;
using MiRs.Domain.Entities.Discord;
using MiRs.Domain.Entities.RuneHunter;
using MiRS.Gateway.DiscordBotClient;

namespace MiRs.DiscordClient
{
    /// <summary>
    /// The client for connection to the Discord Bot API.
    /// </summary>
    public class DiscordBotClient : IDiscordBotClient
    {
        private readonly AppSettings _appSettings;

        public DiscordBotClient(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// The call to send Event Team winner webhook.
        /// </summary>
        /// <param name="team">The winning team</param>
        /// <param name="guildPermissions">the message permissions for the guild</param>
        public async Task SendEventWinningTeam(GuildTeam team, GuildPermissions guildPermissions)
        {
            await _appSettings.DiscordBotDomain
                .AppendPathSegment("v1/Rest")
                .SetQueryParams(new
                {
                    channelId = guildPermissions.ChannelId,
                    winningTeamName = team.TeamName
                })
                .PostAsync();
        }

        /// <summary>
        /// The call to send loot processed alert webhook.
        /// </summary>
        /// <param name="lootAlertDto">The alert to tell discord loot has been updated</param>
        public async Task LatestTeamLootAlert(LootAlertDto lootAlertDto)
        {
            await _appSettings.DiscordBotDomain
               .WithHeader("Content-Type", "application/json")
               .AppendPathSegment($"v1/rest/lootupdate")
               .PostJsonAsync(lootAlertDto);
        }

    }
}
