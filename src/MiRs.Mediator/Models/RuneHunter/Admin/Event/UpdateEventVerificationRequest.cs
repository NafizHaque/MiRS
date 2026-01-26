using MediatR;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.Admin.Event
{
    public class UpdateEventVerificationRequest : IRequest<UpdateEventVerificationResponse>, IValidatable
    {
        public int EventId { get; set; }

        public ulong GuildId { get; set; }

        public string EventPassword { get; set; } = string.Empty;

        public void Validate()
        {
            if (EventId <= 0)
            {
                throw new BadRequestException("Event Id is invalid.");
            }

            if (GuildId <= 0)
            {
                throw new BadRequestException("Guild Id is invalid.");
            }

            if (string.IsNullOrWhiteSpace(EventPassword))
            {
                throw new BadRequestException("Event Password is invalid.");
            }
        }
    }
}
