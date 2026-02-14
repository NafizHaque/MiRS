namespace MiRs.Domain.DTOs.Discord
{
    public class LootAlertDto
    {
        public ulong UserId { get; set; }

        public ulong GuildId { get; set; }

        public ulong ChannelId { get; set; }

        public ulong MessageId { get; set; }
    }
}
