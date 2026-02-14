using Flurl.Http;
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
        /// <summary>
        /// The call to send Event Team winner webhook.
        /// </summary>
        /// <param name="team">The winning team</param>
        /// <param name="guildPermissions">the message permissions for the guild</param>
        public async Task SendEventWinningTeam(GuildTeam team, GuildPermissions guildPermissions)
        {
            await "https://localhost:7265/v1/"
               .WithHeader("Content-Type", "application/json")
               .AppendPathSegment($"rest")
               .PostJsonAsync(new
               {
                   Team = team,
                   Perms = guildPermissions,
               });
        }

        /// <summary>
        /// The call to send loot processed alert webhook.
        /// </summary>
        /// <param name="lootAlertDto">The alert to tell discord loot has been updated</param>
        public async Task LatestTeamLootAlert(LootAlertDto lootAlertDto)
        {
            await "https://localhost:7265/v1/"
               .WithHeader("Content-Type", "application/json")
               .AppendPathSegment($"rest/lootupdate")
               .PostJsonAsync(lootAlertDto);
        }

    }
}
