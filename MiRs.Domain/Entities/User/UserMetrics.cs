using MiRs.Domain.Entities.User.Skills;

namespace MiRs.Domain.Entities.User
{
    /// <summary>
    /// User metrics contains data about the characters: Skills; Bosses; Activities; Compute; 
    /// </summary>
    public class UserMetrics
    {
        /// <summary>
        /// Gets or sets the Skills.
        /// </summary>
        public UserSkills Skills { get; set; } = new UserSkills();

        /// <summary>
        /// Gets or sets the Bosses.
        /// </summary>
        public UserBosses Bosses { get; set; } = new UserBosses();

        /// <summary>
        /// Gets or sets the Activities.
        /// </summary>
        public UserActivities Activities { get; set; } = new UserActivities();

        /// <summary>
        /// Gets or sets the Computed.
        /// </summary>
        public UserComputed Computed { get; set; } = new UserComputed();
    }
}
