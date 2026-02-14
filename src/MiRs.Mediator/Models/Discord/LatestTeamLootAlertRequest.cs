using MediatR;

namespace MiRs.Mediator.Models.Discord
{
    public class LatestTeamLootAlertRequest : IRequest<LatestTeamLootAlertResponse>, IValidatable
    {
        public void Validate()
        {

        }
    }
}
