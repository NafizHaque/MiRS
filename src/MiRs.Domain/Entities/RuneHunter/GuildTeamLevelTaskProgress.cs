using MiRs.Domain.Entities.RuneHunterData;

namespace MiRs.Domain.Entities.RuneHunter
{
    public class GuildTeamLevelTaskProgress
    {
        public int Id { get; set; }

        public int Progress { get; set; }

        public bool IsComplete { get; set; }

        public DateTimeOffset LastUpdated { get; set; }

        public int CategoryLevelProcessId { get; set; }

        public GuildTeamCategoryLevelProgress? CategoryLevelProgress { get; set; }

        public int LevelTaskId { get; set; }

        public LevelTask LevelTask { get; set; }

    }
}
