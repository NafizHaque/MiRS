using MiRs.Domain.Entities.User.Skills;
using Newtonsoft.Json;

namespace MiRs.Domain.Entities.User
{
    class UserMetrics
    {
        /// <summary>
        /// Gets or sets the CreatedAt.
        /// </summary>
        public UserSkills Skills { get; set; } = new UserSkills();

        /// <summary>
        /// Gets or sets the Bosses.
        /// </summary>
        public UserBosses Bosses { get; set; } = new UserBosses();
    }
}
