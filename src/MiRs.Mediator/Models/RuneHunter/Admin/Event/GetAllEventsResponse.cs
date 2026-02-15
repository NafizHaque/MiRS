using MiRs.Domain.DTOs.RuneHunter;

namespace MiRs.Mediator.Models.RuneHunter.Admin.Event
{
    public class GetAllEventsResponse
    {
        /// <summary>
        /// Gets or sets the guild events.
        /// </summary>
        public IList<GameEvent> GuildEvents { get; set; } = new List<GameEvent>();
    }
}
