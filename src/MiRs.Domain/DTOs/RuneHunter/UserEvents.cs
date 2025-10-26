namespace MiRs.Domain.DTOs.RuneHunter
{
    public class UserEvents
    {
        public int Id { get; set; }

        public ulong GuildId { get; set; }

        public string Eventname { get; set; } = string.Empty;

        public DateTimeOffset EventStart { get; set; }

        public DateTimeOffset EventEnd { get; set; }

        public EventTeam EventTeam { get; set; }

    }
}
