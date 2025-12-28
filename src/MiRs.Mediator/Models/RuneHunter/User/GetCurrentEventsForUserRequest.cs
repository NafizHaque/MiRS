using MediatR;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.User
{
    public class GetCurrentEventsForUserRequest : IRequest<GetCurrentEventsForUserResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        public ulong UserId { get; set; }

        /// <summary>
        /// Validates the provided store number.
        /// </summary>
        /// <exception cref="BadRequestException"> The custom exception type for bad requests.</exception>
        public void Validate()
        {
            if (UserId <= 0)
            {
                throw new BadRequestException("Invalid User Id given!");
            }
        }
    }
}
