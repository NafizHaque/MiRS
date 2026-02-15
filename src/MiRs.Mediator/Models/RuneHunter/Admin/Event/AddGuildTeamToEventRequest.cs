using MediatR;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.Admin.Event
{
    public class AddGuildTeamToEventRequest : IRequest<AddGuildTeamToEventResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the event identifier.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [add existing team toggle].
        /// </summary>
        public bool AddExistingTeamToggle { get; set; } = false;

        /// <summary>
        /// Creates new teamtobecreated.
        /// </summary>
        public GuildTeam NewTeamToBeCreated { get; set; } = new GuildTeam();

        /// <summary>
        /// Validate method used to impliment Validations on request arguments.
        /// </summary>
        /// <exception cref="BadRequestException">
        public void Validate()
        {
            if (NewTeamToBeCreated.GuildId <= 0)
            {
                throw new BadRequestException("Guild Id for new team is not valid ");
            }

            if (string.IsNullOrWhiteSpace(NewTeamToBeCreated.TeamName))
            {
                throw new BadRequestException("Team name for new team is not valid");
            }

            if (EventId <= 0)
            {
                throw new BadRequestException("Event Id is not valid ");
            }
        }
    }
}
