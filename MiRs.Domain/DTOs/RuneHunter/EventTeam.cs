using MiRs.Domain.Entities.RuneHunter;

namespace MiRs.Domain.DTOs.RuneHunter
{
    public class EventTeam
    {
        public GuildTeam Teams { get; set; }

        public GuildEvent Events { get; set; }
    }
}
