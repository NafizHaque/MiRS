using MiRs.Domain.Entities.Discord.Enums;

namespace MiRs.Domain.Entities.Discord
{
    public class GuildPermissions
    {
        public int Id { get; set; }

        public ulong GuildId { get; set; }

        public ulong ChannelId { get; set; }

        public string ResponseToken { get; set; }

        public int? TeamId { get; set; }

        public PermissionType Type { get; set; }
    }
}
