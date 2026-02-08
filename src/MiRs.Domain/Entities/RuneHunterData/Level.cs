namespace MiRs.Domain.Entities.RuneHunterData
{
    public class Level
    {
        public int Id { get; set; }

        public int Levelnumber { get; set; }

        public string Unlock { get; set; } = string.Empty;

        public string UnlockDescription { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<LevelTask>? LevelTasks { get; set; }
    }
}
