using MiRs.Domain.DTOs.RuneHunter;

namespace MiRs.Mediator.Models.RuneHunter.Game
{
    public class GetEventTeamProgressForUserResponse
    {
        /// <summary>
        /// Gets or sets the event team progresses.
        /// </summary>
        public IList<EventTeam> EventTeamProgresses { get; set; } = new List<EventTeam>();
    }
}
