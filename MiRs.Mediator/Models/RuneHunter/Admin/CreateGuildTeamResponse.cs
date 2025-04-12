using MiRs.Domain.Entities.RuneHunter;

namespace MiRs.Mediator.Models.RuneHunter.Admin
{
    public class CreateGuildTeamResponse
    {
        public IEnumerable<GuildTeam> GuildTeams { get; set; }
    }
}
