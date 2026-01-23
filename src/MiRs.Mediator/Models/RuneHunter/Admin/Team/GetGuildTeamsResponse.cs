using MiRs.Domain.DTOs.RuneHunter;

namespace MiRs.Mediator.Models.RuneHunter.Admin.Team
{
    public class GetGuildTeamsResponse
    {
        public IEnumerable<GameTeam> GuildTeams { get; set; } = Enumerable.Empty<GameTeam>();
    }
}
