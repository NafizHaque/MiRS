using MiRs.Domain.Entities.RuneHunter;

namespace MiRs.Mediator.Models.RuneHunter.Game
{
    public class GetRecentTeamLootResponse
    {
        public string TeamName { get; set; } = string.Empty;

        public IEnumerable<RHUserRawLoot> Loots { get; set; }
    }
}
