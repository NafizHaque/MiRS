using MiRs.Domain.Exceptions;
using MediatR;

namespace MiRs.Mediator.Models.RuneUser
{
    /// <summary>
    /// The request for getting the Runescape User.
    /// </summary>
    public class GetRuneUserRequest : IRequest<GetRuneUserResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        public string Username { get; set; } = string.Empty;


        /// <summary>
        /// Validates the provided store number.
        /// </summary>
        /// <exception cref="BadRequestException"> The custom exception type for bad requests.</exception>
        public void Validate()
        {
            if (string.IsNullOrEmpty(Username))
            {
                throw new BadRequestException("Username is null or Empty!");
            }
        }
    }
}
