namespace MiRs.Domain.DTOs.RuneHunter
{
    public class EventTeam
    {
        public int Id { get; set; }

        public int TeamId { get; set; }

        public int EventId { get; set; }

        public GameTeam? Team { get; set; }

        public IEnumerable<CategoryProgress>? CategoryProgresses { get; set; }

    }
}
