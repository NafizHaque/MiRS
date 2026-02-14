using MiRs.Domain.DTOs.RuneHunter;

namespace MiRs.Mediator.Models.RuneHunter.Game
{
    public class GetGameMetadataResponse
    {
        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public IEnumerable<CategoryDto> Categories { get; set; } = Enumerable.Empty<CategoryDto>();
    }
}
