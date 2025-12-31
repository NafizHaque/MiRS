using MediatR;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.Game
{
    public class ProcessUserLootRequest : IRequest<ProcessUserLootResponse>, IValidatable
    {

        /// <summary>
        /// validate the ProcessUserLootRequest object.
        /// </summary>
        /// <exception cref="BadRequestException"> The custom exception type for bad requests.</exception>
        public void Validate()
        {
        }
    }
}
