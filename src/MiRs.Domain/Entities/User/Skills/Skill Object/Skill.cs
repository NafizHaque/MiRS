namespace MiRs.Domain.Entities.User.Skills.Skill_Object
{
    /// <summary>
    /// Skill metric object for Users' Skills.
    /// </summary>
    public class Skill
    {
        /// <summary>
        /// Gets or sets the Metric.
        /// </summary>
        public string Metric { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Experience.
        /// </summary>
        public long Experience { get; set; }

        /// <summary>
        /// Gets or sets the Rank.
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets the Level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets the Ehp.
        /// </summary>
        public double Ehp { get; set; }
    }
}
