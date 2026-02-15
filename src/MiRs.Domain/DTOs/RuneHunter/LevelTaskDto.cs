namespace MiRs.Domain.DTOs.RuneHunter
{
    public class LevelTaskDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the goal.
        /// </summary>
        public int Goal { get; set; }

        /// <summary>
        /// Gets or sets the level identifier.
        /// </summary>
        public int LevelId { get; set; }

        /// <summary>
        /// Gets or sets the levelnumber.
        /// </summary>
        public int Levelnumber { get; set; }
    }
}
