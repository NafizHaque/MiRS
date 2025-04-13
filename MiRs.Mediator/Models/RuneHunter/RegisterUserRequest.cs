using MediatR;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter
{
    public class RegisterUserRequest : IRequest<RegisterUserResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the rhUser.
        /// </summary>
        public RHUser? rhUser { get; set; }

        /// <summary>
        /// Validates the user
        /// </summary>
        /// <exception cref="BadRequestException"> The custom exception type for bad requests.</exception>
        public void Validate()
        {
            if (rhUser.UserId <= 0)
            {
                throw new BadRequestException("Invalid Id given!");
            }

            if (string.IsNullOrEmpty(rhUser.Username))
            {
                throw new BadRequestException("Username is null or Empty!");
            }
        }
    }
}

