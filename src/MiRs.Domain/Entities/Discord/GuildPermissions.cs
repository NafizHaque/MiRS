using MiRs.Domain.Entities.Discord.Enums;

namespace MiRs.Domain.Entities.Discord
{
    public class GuildPermissions
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the guild identifier.
        /// </summary>
        public ulong GuildId { get; set; }

        /// <summary>
        /// Gets or sets the channel identifier.
        /// </summary>
        public ulong ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public PermissionType Type { get; set; }

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        public ulong? MessageId { get; set; }

        /// <summary>
        /// Gets or sets the team identifier.
        /// </summary>
        public int? TeamId { get; set; }

    }
}
