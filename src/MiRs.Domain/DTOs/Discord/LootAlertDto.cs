namespace MiRs.Domain.DTOs.Discord
{
    public class LootAlertDto
    {
        public ulong UserId { get; set; }

        public ulong GuildId { get; set; }

        public ulong ResponseId { get; set; }

        public string ResponseToken { get; set; } = string.Empty;
    }
}
