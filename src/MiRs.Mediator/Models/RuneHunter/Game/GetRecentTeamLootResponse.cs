using MiRs.Domain.Entities.RuneHunter;

namespace MiRs.Mediator.Models.RuneHunter.Game
{
    public class GetRecentTeamLootResponse
    {
        /// <summary>
        /// Gets or sets the name of the team.
        /// </summary>
        public string TeamName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the loots.
        /// </summary> 
        public IEnumerable<RHUserRawLoot> Loots { get; set; }
    }
}
