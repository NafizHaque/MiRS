namespace MiRs.Domain.Entities.User.Skills.Skill_Object
{
    /// <summary>
    /// Compute metric object for Users' Computed.
    /// </summary>
    public class Compute
    {
        /// <summary>
        /// Gets or sets the Metric.
        /// </summary>
        public string Metric { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Kills.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the Rank.
        /// </summary>
        public int Rank { get; set; }
    }
}
