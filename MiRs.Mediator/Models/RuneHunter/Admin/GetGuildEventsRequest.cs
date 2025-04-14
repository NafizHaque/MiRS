using MediatR;
using MiRs.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Mediator.Models.RuneHunter.Admin
{
    public class GetGuildEventsRequest : IRequest<GetGuildEventsResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the GuildId.
        /// </summary>
        public ulong GuildId { get; set; }

        /// <summary>
        /// Validates the Guild Id.
        /// </summary>
        /// <exception cref="BadRequestException"> The custom exception type for bad requests.</exception>
        public void Validate()
        {
            if (GuildId <= 0)
            {
                throw new BadRequestException("Invalid Guild Id given!");
            }
        }
    }
}
