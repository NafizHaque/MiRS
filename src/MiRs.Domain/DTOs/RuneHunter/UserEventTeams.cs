using MiRs.Domain.Entities.RuneHunter;

namespace MiRs.Domain.DTOs.RuneHunter
{
    public class UserEventTeams
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public RHUser? User { get; set; }

        /// <summary>
        /// Gets or sets the event teams.
        /// </summary>
        public IEnumerable<EventTeam> EventTeams { get; set; } = Enumerable.Empty<EventTeam>();
    }
}
