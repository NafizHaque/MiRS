namespace MiRs.Domain.DTOs.RuneHunter
{
    public class CategoryProgress
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
        /// Gets or sets the category identifier.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public CategoryDto? Category { get; set; }

        /// <summary>
        /// Gets or sets the category level process.
        /// </summary>
        public IEnumerable<CategoryLevelProgress>? CategoryLevelProcess { get; set; }
    }
}
