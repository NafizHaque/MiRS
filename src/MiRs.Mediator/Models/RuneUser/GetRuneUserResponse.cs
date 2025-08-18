using MiRs.Domain.Entities.User;

namespace MiRs.Mediator.Models.RuneUser
{
    /// <summary>
    /// The response for getting users by a given username(RSN).
    /// </summary>
    public class GetRuneUserResponse
    {
        /// <summary>
        /// Gets or sets the UserRetrieved object.
        /// </summary>
        public User? UserRetrieved { get; set; }
    }
}
