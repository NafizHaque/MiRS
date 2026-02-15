namespace MiRs.Domain.DTOs.RuneHunter
{
    public class UserToTeam
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public ulong UserId { get; set; }

        /// <summary>
        /// Gets or sets the team identifier.
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public GameUser? User { get; set; }

    }
}
