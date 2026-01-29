using MiRs.Domain.Entities.RuneHunter;

namespace MiRs.API.ApiDTOs
{
    public class UpdateTeamList
    {
        public int EventId { get; set; }

        public IEnumerable<GuildTeam> CurrentTeamsToBeUpdated { get; set; } = Enumerable.Empty<GuildTeam>();
    }
}
