using MiRs.Domain.Entities.RuneHunter;

namespace MiRs.Mediator.Models.RuneHunter.Admin
{
    public class GetGuildEventsResponse
    {
        /// <summary>
        /// Gets or sets the GuildEvents.
        /// </summary>
        public IEnumerable<GuildEvent> GuildEvents { get; set; } = Enumerable.Empty<GuildEvent>();
    }
}
