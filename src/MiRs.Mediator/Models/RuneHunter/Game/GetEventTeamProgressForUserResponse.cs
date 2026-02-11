using MiRs.Domain.DTOs.RuneHunter;

namespace MiRs.Mediator.Models.RuneHunter.Game
{
    public class GetEventTeamProgressForUserResponse
    {
        public IList<EventTeam> EventTeamProgresses { get; set; } = new List<EventTeam>();
    }
}
