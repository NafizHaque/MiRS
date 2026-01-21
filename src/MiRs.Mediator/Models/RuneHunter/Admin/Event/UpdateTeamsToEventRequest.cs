using MediatR;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.Admin.Event
{
    public class UpdateTeamsToEventRequest : IRequest<UpdateTeamsToEventResponse>, IValidatable
    {
        public int EventId { get; set; }

        public bool AddExistingTeamToggle { get; set; } = false;

        public GuildTeam NewTeamToBeCreated { get; set; } = new GuildTeam();

        public IEnumerable<GuildTeam> CurrentTeamsToBeUpdated { get; set; } = Enumerable.Empty<GuildTeam>();

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
