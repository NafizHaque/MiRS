namespace MiRs.Domain.Entities.RuneHunter
{
    public class GuildEventTeam
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public GuildEvent? Event { get; set; }

        public int TeamId { get; set; }

        public GuildTeam? Team { get; set; }

        public ICollection<GuildTeamCategoryProgress>? CategoryProgresses { get; set; }

    }
}
