using MiRs.Domain.Entities.User.Skills.Skill_Object;

namespace MiRs.Domain.Entities.User.Skills
{
    /// <summary>
    /// User Computed contains data about the users Ehb and Ehp
    /// </summary>
    public class UserComputed
    {
        /// <summary>
        /// Gets or sets the Ehp.
        /// </summary>
        public Compute Ehp { get; set; } = new Compute();

        /// <summary>
        /// Gets or sets the Ehb.
        /// </summary>
        public Compute Ehb { get; set; } = new Compute();
    }
}
