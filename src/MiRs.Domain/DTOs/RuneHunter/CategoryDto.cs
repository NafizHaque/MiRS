namespace MiRs.Domain.DTOs.RuneHunter
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public IEnumerable<LevelDto>? Levels { get; set; }
    }
}
