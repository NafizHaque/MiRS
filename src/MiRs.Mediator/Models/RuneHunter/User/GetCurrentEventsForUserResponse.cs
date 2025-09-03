using MiRs.Domain.Entities.RuneHunter;

namespace MiRs.Mediator.Models.RuneHunter.User
{
    public class GetCurrentEventsForUserResponse
    {
        /// <summary>
        /// Gets or sets a list of the Users current events
        /// </summary>
        public IEnumerable<GuildEvent> UserCurrentEvents { get; set; } =  Enumerable.Empty<GuildEvent>();
    }
}
