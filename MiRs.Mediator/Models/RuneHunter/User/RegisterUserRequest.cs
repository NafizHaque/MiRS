using MediatR;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.User
{
    public class RegisterUserRequest : IRequest<RegisterUserResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the rhUser.
        /// </summary>
        public RHUser? rhUserToBeCreated { get; set; }

        /// <summary>
        /// Validates the user
        /// </summary>
        /// <exception cref="BadRequestException"> The custom exception type for bad requests.</exception>
        public void Validate()
        {
            if (rhUserToBeCreated.UserId <= 0)
            {
                throw new BadRequestException("Invalid Id given!");
            }

            if (string.IsNullOrEmpty(rhUserToBeCreated.Username))
            {
                throw new BadRequestException("Username is null or Empty!");
            }

            if (string.IsNullOrEmpty(rhUserToBeCreated.Runescapename))
            {
                throw new BadRequestException("Runescapename is null or Empty!");
            }
        }
    }
}

