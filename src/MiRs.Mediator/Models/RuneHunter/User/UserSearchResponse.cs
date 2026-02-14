using MiRs.Domain.DTOs.RuneHunter;

namespace MiRs.Mediator.Models.RuneHunter.User
{
    /// <summary>
    /// The response for User search.
    /// </summary>
    public class UserSearchResponse
    {
        /// <summary>
        /// Gets or sets the list of Users.
        /// </summary>
        public IEnumerable<GameUser> Users { get; set; } = Enumerable.Empty<GameUser>();
    }
}
