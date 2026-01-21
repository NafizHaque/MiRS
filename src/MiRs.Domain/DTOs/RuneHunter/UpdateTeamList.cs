using MiRs.Domain.Entities.RuneHunter;

namespace MiRs.Domain.DTOs.RuneHunter
{
    public class UpdateTeamList
    {
        public int EventId { get; set; }

        public bool AddExistingTeamToggle { get; set; } = false;

        public GuildTeam NewTeamToBeCreated { get; set; } = new GuildTeam();

        public IEnumerable<GuildTeam> CurrentTeamsToBeUpdated { get; set; } = Enumerable.Empty<GuildTeam>();
    }
}
