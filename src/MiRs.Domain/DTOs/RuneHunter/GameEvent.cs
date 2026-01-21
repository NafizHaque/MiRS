namespace MiRs.Domain.DTOs.RuneHunter
{
    public class GameEvent
    {
        public int Id { get; set; }

        public ulong GuildId { get; set; }

        public string Eventname { get; set; } = string.Empty;

        public bool EventActive { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset EventStart { get; set; }

        public DateTimeOffset EventEnd { get; set; }

        public int TotalPlayers { get; set; }
    }
}
