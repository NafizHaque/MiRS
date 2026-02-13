using MediatR;
using MiRs.Domain.Entities.Discord.Enums;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.Discord
{
    public class GuildPermissionsRequest : IRequest<GuildPermissionsResponse>, IValidatable
    {
        public ulong GuildId { get; set; }

        public PermissionType permissionType { get; set; }

        public void Validate()
        {
            if (GuildId <= 0)
            {
                throw new BadRequestException("Invalid GuildId");
            }
        }
    }
}
