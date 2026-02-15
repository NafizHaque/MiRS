namespace MiRs.Domain.DTOs.RuneHunter
{
    public class EventTeam
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the team identifier.
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// Gets or sets the event identifier.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets the team.
        /// </summary>
        public GameTeam? Team { get; set; }

        /// <summary>
        /// Gets or sets the category progresses.
        /// </summary>
        public IEnumerable<CategoryProgress>? CategoryProgresses { get; set; }

    }
}
