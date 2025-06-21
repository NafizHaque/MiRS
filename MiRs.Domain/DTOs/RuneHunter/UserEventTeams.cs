using MiRs.Domain.Entities.RuneHunter;

namespace MiRs.Domain.DTOs.RuneHunter
{
    public class UserEventTeams
    {
        public RHUser? User { get; set; }

        public IEnumerable<EventTeam> EventTeams { get; set; }
    }
}
