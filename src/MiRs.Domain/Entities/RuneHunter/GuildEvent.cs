namespace MiRs.Domain.Entities.RuneHunter
{
    public class GuildEvent
    {
        public int Id { get; set; }

        public ulong GuildId { get; set; }

        public string Eventname { get; set; } = string.Empty;

        public string ParticipantPassword { get; set; } = string.Empty;

        public bool EventActive { get; set; }

        public string EventPassword { get; set; } = string.Empty;

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset EventStart { get; set; }

        public DateTimeOffset EventEnd { get; set; }

        public ICollection<GuildEventTeam>? EventTeams { get; set; }
    }
}
