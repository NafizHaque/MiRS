using MediatR;

namespace MiRs.Mediator.Models.RuneHunter.Game
{
    public class ProcessUserLootRequest : IRequest<ProcessUserLootResponse>, IValidatable
    {
        public void Validate()
        {
        }
    }
}
