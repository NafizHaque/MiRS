namespace MiRs.Domain.DTOs.RuneHunter
{
    public class GameTeam
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
        /// Gets or sets the name of the team.
        /// </summary>
        public string TeamName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the users in team.
        /// </summary>
        public IEnumerable<UserToTeam> UsersInTeam { get; set; } = Enumerable.Empty<UserToTeam>();
    }
}
