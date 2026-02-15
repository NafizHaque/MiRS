namespace MiRs.Domain.DTOs.RuneHunter
{
    public class LevelDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the levelnumber.
        /// </summary>
        public int Levelnumber { get; set; }

        /// <summary>
        /// Gets or sets the unlock.
        /// </summary>
        public string Unlock { get; set; }

        /// <summary>
        /// Gets or sets the unlock description.
        /// </summary>
        public string UnlockDescription { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the level tasks.
        /// </summary>
        public IEnumerable<LevelTaskDto>? LevelTasks { get; set; }
    }
}
