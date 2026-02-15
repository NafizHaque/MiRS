namespace MiRs.Domain.DTOs.RuneHunter
{
    public class LevelTaskProgress
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the progress.
        /// </summary>
        public int Progress { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is complete.
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        public DateTimeOffset LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the guild event team identifier.
        /// </summary>
        public int GuildEventTeamId { get; set; }

        /// <summary>
        /// Gets or sets the category level process identifier.
        /// </summary>
        public int CategoryLevelProcessId { get; set; }

        /// <summary>
        /// Gets or sets the level task identifier.
        /// </summary>
        public int LevelTaskId { get; set; }

        /// <summary>
        /// Gets or sets the level task.
        /// </summary>
        public LevelTaskDto LevelTask { get; set; }
    }
}
