namespace MiRs.Domain.DTOs.RuneHunter
{
    public class CategoryProgress
    {
        public int Id { get; set; }

        public bool IsComplete { get; set; }

        public int CategoryId { get; set; }

        public CategoryDto? Category { get; set; }

        public IEnumerable<CategoryLevelProgress>? CategoryLevelProcess { get; set; }
    }
}
