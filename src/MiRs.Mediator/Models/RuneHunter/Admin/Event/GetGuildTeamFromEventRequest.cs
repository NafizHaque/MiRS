using MediatR;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.Admin.Event
{
    public class GetGuildTeamFromEventRequest : IRequest<GetGuildTeamFromEventResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the EventId.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// validate the GuildEventToBeCreated object.
        /// </summary>
        /// <exception cref="BadRequestException"> The custom exception type for bad requests.</exception>
        public void Validate()
        {
            if (EventId <= 0)
            {
                throw new BadRequestException("Invalid Event Id given!");
            }
        }
    }
}
