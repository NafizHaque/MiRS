namespace MiRs.Domain.DTOs.RuneHunter
{
    public class CategoryLevelProgress
    {
        public int Id { get; set; }

        public bool IsComplete { get; set; }

        public bool IsActive { get; set; }

        public DateTimeOffset LastUpdated { get; set; }

        public int LevelId { get; set; }

        public LevelDto? Level { get; set; }

        public int CategoryProgressId { get; set; }

        public IEnumerable<LevelTaskProgress>? LevelTaskProgress { get; set; }
    }
}
