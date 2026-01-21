namespace MiRs.Domain.DTOs.RuneHunter
{
    public class GameTeam
    {
        public int Id { get; set; }

        public ulong GuildId { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public DateTimeOffset CreatedDate { get; set; }

        public IEnumerable<UserToTeam> UsersInTeam { get; set; } = Enumerable.Empty<UserToTeam>();
    }
}
