using MediatR;

namespace MiRs.Mediator.Models.RuneHunter.Admin.Event
{
    public class GetAllEventsRequest : IRequest<GetAllEventsResponse>, IValidatable
    {
        public void Validate()
        {
        }
    }
}
