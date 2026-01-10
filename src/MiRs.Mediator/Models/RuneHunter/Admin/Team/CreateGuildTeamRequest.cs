using MediatR;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.Admin.Team
{
    public class CreateGuildTeamRequest : IRequest<CreateGuildTeamResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the GuildId.
        /// </summary>
        public ulong GuildId { get; set; }

        /// <summary>
        /// Gets or sets the TeamName.
        /// </summary>
        public string Teamname { get; set; } = string.Empty;

        /// <summary>
        /// Validates the user
        /// </summary>
        /// <exception cref="BadRequestException"> The custom exception type for bad requests.</exception>
        public void Validate()
        {
            if (GuildId <= 0)
            {
                throw new BadRequestException("Invalid Guild Id given!");
            }

            if (string.IsNullOrEmpty(Teamname))
            {
                throw new BadRequestException("Teamname is null or Empty!");
            }
        }
    }
}
