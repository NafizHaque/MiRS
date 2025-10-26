using MiRs.Domain.DTOs.RuneHunter;
using MiRs.Domain.Entities.RuneHunter;

namespace MiRs.Mediator.Models.RuneHunter.User
{
    public class GetCurrentEventsForUserResponse
    {
        /// <summary>
        /// Gets or sets a list of the Users current events
        /// </summary>
        public IEnumerable<UserEvents> UserCurrentEvents { get; set; } =  Enumerable.Empty<UserEvents>();
    }
}
