using MediatR;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.Game
{
    public class GetRecentTeamLootRequest : IRequest<GetRecentTeamLootResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public ulong UserId { get; set; }

        /// <summary>
        /// Gets or sets the guild identifier.
        /// </summary>
        public ulong GuildId { get; set; }

        /// <summary>
        /// Gets or sets the channel identifier.
        /// </summary>
        public ulong? ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        public ulong? MessageId { get; set; }

        /// <summary>
        /// Validate method used to impliment Validations on request arguments.
        /// </summary>
        /// <exception cref="BadRequestException"></exception>
        public void Validate()
        {
            if (UserId <= 0)
            {
                throw new BadRequestException("User id is null or missing!");
            }

            if (GuildId <= 0)
            {
                throw new BadRequestException("Guild id is null or missing!");
            }
        }
    }
}
