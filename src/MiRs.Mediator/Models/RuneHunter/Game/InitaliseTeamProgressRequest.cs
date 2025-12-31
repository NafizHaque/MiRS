using MediatR;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.Game
{
    public class InitaliseTeamProgressRequest : IRequest<InitaliseTeamProgressResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the TeamId.
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// Gets or sets the EventId.
        /// </summary>
        public int EventId { get; set; }
        /// <summary>
        /// validate the InitaliseTeamProgressRequest object.
        /// </summary>
        /// <exception cref="BadRequestException"> The custom exception type for bad requests.</exception>
        public void Validate()
        {
        }
    }
}
