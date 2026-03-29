using MiRs.Domain.Entities.RuneHunter;

namespace MiRs.Mediator.Models.RuneHunter.User
{
    public class GetLatestUserLootResponse
    {
        /// <summary>
        /// Gets or sets the loots.
        /// </summary> 
        public IEnumerable<RHUserRawLoot> Loots { get; set; }
    }
}
