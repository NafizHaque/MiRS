namespace MiRs.Domain.DTOs.RuneHunter
{
    public class LevelDto
    {
        public int Id { get; set; }

        public int Levelnumber { get; set; }

        public string Unlock { get; set; }

        public string UnlockDescription { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public IEnumerable<LevelTaskDto>? LevelTasks { get; set; }
    }
}
