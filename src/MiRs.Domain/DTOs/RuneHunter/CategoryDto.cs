namespace MiRs.Domain.DTOs.RuneHunter
{
    public class CategoryDto
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
        /// Gets or sets the levels.
        /// </summary>
        public IEnumerable<LevelDto>? Levels { get; set; }
    }
}
