using MiRs.Domain.DTOs.Discord;
using MiRs.Domain.Entities.Discord;
using MiRs.Domain.Entities.RuneHunter;

namespace MiRS.Gateway.DiscordBotClient
{
    public interface IDiscordBotClient
    {

        public Task SendEventWinningTeam(GuildTeam team, GuildPermissions perms);

        public Task LatestTeamLootAlert(LootAlertDto lootAlertDto);
    }
}
