using MediatR;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.Admin
{
    public class GetGuildTeamsRequest : IRequest<GetGuildTeamsResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the GuildId.
        /// </summary>
        public int GuildId { get; set; }

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
