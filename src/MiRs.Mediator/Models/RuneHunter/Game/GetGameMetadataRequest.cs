using MediatR;

namespace MiRs.Mediator.Models.RuneHunter.Game
{
    public class GetGameMetadataRequest : IRequest<GetGameMetadataResponse>, IValidatable
    {
        public void Validate() { }
    }
}
