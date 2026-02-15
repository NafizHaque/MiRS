namespace MiRs.Domain.DTOs.Discord
{

    public class LootAlertDto
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
        public ulong ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        public ulong MessageId { get; set; }
    }
}
