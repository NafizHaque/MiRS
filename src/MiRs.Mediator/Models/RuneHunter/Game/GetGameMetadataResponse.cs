using MiRs.Domain.DTOs.RuneHunter;

namespace MiRs.Mediator.Models.RuneHunter.Game
{
    public class GetGameMetadataResponse
    {
        public IEnumerable<CategoryDto> Categories { get; set; } = Enumerable.Empty<CategoryDto>();
    }
}
