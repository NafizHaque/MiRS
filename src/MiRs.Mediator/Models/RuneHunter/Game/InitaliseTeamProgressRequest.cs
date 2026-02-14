using MediatR;

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

        public void Validate()
        {
        }
    }
}
