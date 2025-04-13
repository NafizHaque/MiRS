using MediatR;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Entities.User;
using MiRs.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Mediator.Models.RuneHunter.Admin
{
    public class CreateEventInGuildRequest : IRequest<CreateEventInGuildResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the GuildEvent object to be created.
        /// </summary>
        public GuildEvent? GuildEventToBeCreated { get; set; }

        /// <summary>
        /// validate the GuildEventToBeCreated object.
        /// </summary>
        /// <exception cref="BadRequestException"> The custom exception type for bad requests.</exception>
        public void Validate()
        {
            if (GuildEventToBeCreated == null)
            {
                throw new BadRequestException("Event is null. Event must have a value.");
            }

            if (string.IsNullOrEmpty(GuildEventToBeCreated.Eventname))
            {
                throw new BadRequestException("Eventname must have a valid name.");
            }

            if (GuildEventToBeCreated.EventStart > GuildEventToBeCreated.EventEnd)
            {
                throw new BadRequestException("Start date must be less than End date");
            }
        }
    }
}
