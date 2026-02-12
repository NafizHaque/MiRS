using MiRs.Domain.Entities.RuneHunter;

namespace MiRS.Gateway.DiscordBotClient
{
    public interface IDiscordBotClient
    {

        public Task SendEventWinningTeam(GuildTeam team);
    }
}
