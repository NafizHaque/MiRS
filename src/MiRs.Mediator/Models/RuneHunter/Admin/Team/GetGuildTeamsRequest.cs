using MediatR;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.Admin.Team
{
    public class GetGuildTeamsRequest : IRequest<GetGuildTeamsResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the GuildId.
        /// </summary>
        public ulong GuildId { get; set; }

        /// <summary>
        /// Validates the Guild Id.
        /// </summary>
        /// <exception cref="BadRequestException"> The custom exception type for bad requests.</exception>
        public void Validate()
        {
            if (GuildId <= 0)
            {
                throw new BadRequestException("Invalid Guild Id given!");
            }
        }
    }
}
