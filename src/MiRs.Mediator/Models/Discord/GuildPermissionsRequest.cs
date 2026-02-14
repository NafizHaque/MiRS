using MediatR;
using MiRs.Domain.Entities.Discord.Enums;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.Discord
{
    public class GuildPermissionsRequest : IRequest<GuildPermissionsResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the guild identifier.
        /// </summary>
        public ulong GuildId { get; set; }

        /// <summary>
        /// Gets or sets the type of the permission.
        /// </summary>
        public PermissionType permissionType { get; set; }

        /// <summary>
        /// Validate method used to impliment Validations on request arguments.
        /// </summary>
        /// <exception cref="BadRequestException">Invalid GuildId</exception>
        public void Validate()
        {
            if (GuildId <= 0)
            {
                throw new BadRequestException("Invalid GuildId");
            }
        }
    }
}
