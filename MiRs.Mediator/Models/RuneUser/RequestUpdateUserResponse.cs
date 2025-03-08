using MiRs.Domain.Entities.User;

namespace MiRs.Mediator.Models.RuneUser
{
    /// <summary>
    /// The response for updating and retrieving Users latest data point.
    /// </summary>
    public class RequestUpdateUserResponse
    {
        /// <summary>
        /// Gets or sets the UserRetrieved object.
        /// </summary>
        public User? UserRetrieved { get; set; }
    }
}
