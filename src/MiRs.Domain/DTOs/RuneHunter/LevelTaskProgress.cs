namespace MiRs.Domain.DTOs.RuneHunter
{
    public class LevelTaskProgress
    {
        public int Id { get; set; }

        public int Progress { get; set; }

        public bool IsComplete { get; set; }

        public DateTimeOffset LastUpdated { get; set; }

        public int GuildEventTeamId { get; set; }

        public int CategoryLevelProcessId { get; set; }

        public int LevelTaskId { get; set; }

        public LevelTaskDto LevelTask { get; set; }
    }
}
