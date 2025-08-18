using MediatR;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.User
{
    public class JoinTeamRequest : IRequest<JoinTeamResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the userid.
        /// </summary>
        public ulong UserId { get; set; }

        /// <summary>
        /// Gets or sets the guild id.
        /// </summary>
        public ulong GuildId { get; set; }

        /// <summary>
        /// Gets or sets the teamName.
        /// </summary>
        public string Teamname { get; set; }

        /// <summary>
        /// Validates the user
        /// </summary>
        /// <exception cref="BadRequestException"> The custom exception type for bad requests.</exception>
        public void Validate()
        {
            if (UserId <= 0)
            {
                throw new BadRequestException("Invalid user Id given!");
            }

            if (GuildId <= 0)
            {
                throw new BadRequestException("Invalid guild Id given!");
            }

            if (string.IsNullOrEmpty(Teamname))
            {
                throw new BadRequestException("Username is null or Empty!");
            }
        }
    }
}
