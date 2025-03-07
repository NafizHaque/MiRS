using MiRs.Domain.Entities.User.Skills.Skill_Object;

namespace MiRs.Domain.Entities.User.Skills
{
    /// <summary>
    /// User Bosses contains a Dictionary With <Key,Value> pair of Bossname to Boss object.
    /// </summary>
    public class UserBosses
    {
        /// <summary>
        /// Gets or sets the Bos Dictionary.
        /// </summary>
        public Dictionary<string, Boss>? BossDict { get; set; }
    }
}
