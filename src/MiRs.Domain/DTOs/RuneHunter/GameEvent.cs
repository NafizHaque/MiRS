namespace MiRs.Domain.DTOs.RuneHunter
{
    public class GameEvent
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
        /// Gets or sets the eventname.
        /// </summary>
        public string Eventname { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether [event active].
        /// </summary>
        public bool EventActive { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the event start.
        /// </summary>
        public DateTimeOffset EventStart { get; set; }

        /// <summary>
        /// Gets or sets the event end.
        /// </summary>
        public DateTimeOffset EventEnd { get; set; }

        /// <summary>
        /// Gets or sets the total players.
        /// </summary>
        public int TotalPlayers { get; set; }
    }
}
