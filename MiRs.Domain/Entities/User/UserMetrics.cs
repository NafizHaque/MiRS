using MiRs.Domain.Entities.User.Skills;

namespace MiRs.Domain.Entities.User
{
    class UserMetrics
    {
        /// <summary>
        /// Gets or sets the CreatedAt.
        /// </summary>
        public UserSkills Skills { get; set; } = new UserSkills();
    }
}
