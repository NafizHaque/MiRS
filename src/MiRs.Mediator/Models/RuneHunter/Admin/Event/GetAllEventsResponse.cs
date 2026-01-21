using MiRs.Domain.DTOs.RuneHunter;

namespace MiRs.Mediator.Models.RuneHunter.Admin.Event
{
    public class GetAllEventsResponse
    {
        public IList<GameEvent> GuildEvents { get; set; } = new List<GameEvent>();
    }
}
