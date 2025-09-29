using MediatR;
using Microsoft.Extensions.Options;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.Game
{
    public class LogUserLootRequest : IRequest<LogUserLootResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the LootMessage.
        /// </summary>
        public string LootMessage { get; set; }

        /// <summary>
        /// validate the LogUserLootRequest object.
        /// </summary>
        /// <exception cref="BadRequestException"> The custom exception type for bad requests.</exception>
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(LootMessage))
            {
                throw new BadRequestException("Loot message is empty!");
            }
        }
    }
}
