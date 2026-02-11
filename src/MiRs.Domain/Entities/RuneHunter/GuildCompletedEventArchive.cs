namespace MiRs.Domain.Entities.RuneHunter
{
    public class GuildCompletedEventArchive
    {
        public int Id { get; set; }

        public ulong GuildId { get; set; }

        public string Eventname { get; set; } = string.Empty;

        public bool EventComplete { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset EventStart { get; set; }

        public DateTimeOffset EventEnd { get; set; }

        public string EventTeamWinner { get; set; } = string.Empty;
    }
}
