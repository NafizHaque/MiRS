namespace MiRs.Domain.DTOs.RuneHunter
{
    public class GameUser
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public ulong UserId { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the previous username.
        /// </summary>
        public string PreviousUsername { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the runescapename.
        /// </summary>
        public string Runescapename { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the previous runescapename.
        /// </summary>
        public string PreviousRunescapename { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }
    }
}
