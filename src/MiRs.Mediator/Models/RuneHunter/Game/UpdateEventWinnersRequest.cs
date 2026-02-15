using MediatR;

namespace MiRs.Mediator.Models.RuneHunter.Game
{
    public class UpdateEventWinnersRequest : IRequest<UpdateEventWinnersResponse>, IValidatable
    {
        public void Validate()
        { }
    }
}
