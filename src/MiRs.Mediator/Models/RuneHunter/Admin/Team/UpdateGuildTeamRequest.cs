using MediatR;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.Admin.Team
{
    public class UpdateGuildTeamRequest : IRequest<UpdateGuildTeamResponse>, IValidatable
    {

        /// <summary>
        /// Gets or sets the TeamToBeUpdated.
        /// </summary>
        public GuildTeam TeamToBeUpdated { get; set; }

        /// <summary>
        /// validate the GuildEventToBeCreated object.
        /// </summary>
        /// <exception cref="BadRequestException"> The custom exception type for bad requests.</exception>
        public void Validate()
        {
            if (TeamToBeUpdated.Id <= 0)
            {
                throw new BadRequestException("Invalid Team Id given!");
            }

            if (string.IsNullOrWhiteSpace(TeamToBeUpdated.TeamName))
            {
                throw new BadRequestException("Invalid Team name given!");
            }
        }
    }
}
