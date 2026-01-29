using MediatR;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.Admin.Event
{
    public class UpdateTeamsToEventRequest : IRequest<UpdateTeamsToEventResponse>, IValidatable
    {
        public int EventId { get; set; }

        public IEnumerable<GuildTeam> CurrentTeamsToBeUpdated { get; set; } = Enumerable.Empty<GuildTeam>();

        public void Validate()
        {
            if (EventId <= 0)
            {
                throw new BadRequestException("Event Id is not valid ");
            }
        }
    }
}
