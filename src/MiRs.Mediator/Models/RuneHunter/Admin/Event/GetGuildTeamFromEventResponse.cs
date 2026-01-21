using MiRs.Domain.DTOs.RuneHunter;

namespace MiRs.Mediator.Models.RuneHunter.Admin.Event
{
    public class GetGuildTeamFromEventResponse
    {
        public IEnumerable<GameTeam> GuildTeams { get; set; } = Enumerable.Empty<GameTeam>();
    }
}
