using MiRs.Domain.Entities.RuneHunter;

namespace MiRs.Mediator.Models.RuneHunter.Admin.Event
{
    public class CreateEventInGuildResponse
    {
        public GuildEvent? CreatedEvent { get; set; }
    }
}
