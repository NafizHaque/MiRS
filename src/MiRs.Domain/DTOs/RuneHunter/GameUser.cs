namespace MiRs.Domain.DTOs.RuneHunter
{
    public class GameUser
    {
        public ulong UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PreviousUsername { get; set; } = string.Empty;

        public string Runescapename { get; set; } = string.Empty;

        public string PreviousRunescapename { get; set; } = string.Empty;

        public DateTimeOffset CreatedDate { get; set; }
    }
}
