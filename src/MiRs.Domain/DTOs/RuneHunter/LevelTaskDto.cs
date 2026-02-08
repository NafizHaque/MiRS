namespace MiRs.Domain.DTOs.RuneHunter
{
    public class LevelTaskDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Goal { get; set; }

        public int LevelId { get; set; }

        public int Levelnumber { get; set; }
    }
}
