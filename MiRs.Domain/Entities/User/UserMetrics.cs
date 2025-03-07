using MiRs.Domain.Entities.User.Skills;

namespace MiRs.Domain.Entities.User
{
    public class UserMetrics
    {
        /// <summary>
        /// Gets or sets the CreatedAt.
        /// </summary>
        public UserSkills Skills { get; set; } = new UserSkills();

        /// <summary>
        /// Gets or sets the Bosses.
        /// </summary>
        public UserBosses Bosses { get; set; } = new UserBosses();

        /// <summary>
        /// Gets or sets the Bosses.
        /// </summary>
        public UserActivities Activities { get; set; } = new UserActivities();
    }
}
