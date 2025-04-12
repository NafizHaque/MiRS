using MiRs.Domain.Entities.RuneHunterData;

namespace MiRs.Domain.Entities.RuneHunter
{
    public class GuildTeamLevelProgress
    {
        public int Id { get; set; }

        public int Progress { get; set; }

        public bool IsComplete { get; set; }

        public bool IsActive { get; set; }

        public DateTimeOffset LastUpdated { get; set; }

        public int GuildEventTeamId { get; set; }

        public GuildEventTeam GuildEventTeam { get; set; }

        public int LevelId { get; set; }

        public LevelTask Level { get; set; }

    }
}
