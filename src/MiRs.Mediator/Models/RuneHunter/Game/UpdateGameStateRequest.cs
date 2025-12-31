using MediatR;

namespace MiRs.Mediator.Models.RuneHunter.Game
{
    public class UpdateGameStateRequest : IRequest<UpdateGameStateResponse>, IValidatable
    {
        public void Validate() { }
    }
}
