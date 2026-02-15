namespace MiRs.Domain.DTOs.RuneHunter
{
    public class CategoryLevelProgress
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is complete.
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        public DateTimeOffset LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the level identifier.
        /// </summary>
        public int LevelId { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        public LevelDto? Level { get; set; }

        /// <summary>
        /// Gets or sets the category progress identifier.
        /// </summary>
        public int CategoryProgressId { get; set; }

        /// <summary>
        /// Gets or sets the level task progress.
        /// </summary>
        public IEnumerable<LevelTaskProgress>? LevelTaskProgress { get; set; }
    }
}
