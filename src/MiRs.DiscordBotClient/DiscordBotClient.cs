using Flurl.Http;
using MiRs.Domain.DTOs.Discord;
using MiRs.Domain.Entities.Discord;
using MiRs.Domain.Entities.RuneHunter;
using MiRS.Gateway.DiscordBotClient;

namespace MiRs.DiscordClient
{
    public class DiscordBotClient : IDiscordBotClient
    {
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

        public async Task LatestTeamLootAlert(LootAlertDto lootAlertDto)
        {
            await "https://localhost:7265/v1/"
               .WithHeader("Content-Type", "application/json")
               .AppendPathSegment($"rest/lootupdate")
               .PostJsonAsync(lootAlertDto);
        }

    }
}
