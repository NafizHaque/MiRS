using MiRs.Domain.Entities.RuneHunterData;

namespace MiRs.Domain.Entities.RuneHunter
{
    public class GuildTeamCategoryProgress
    {
        public int Id { get; set; }

        public bool IsComplete { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public int GuildEventTeamId { get; set; }

        public GuildEventTeam? GuildEventTeam { get; set; }

        public ICollection<GuildTeamCategoryLevelProgress>? CategoryLevelProcess { get; set; }
    }
}
