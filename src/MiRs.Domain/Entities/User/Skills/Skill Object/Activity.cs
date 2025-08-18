namespace MiRs.Domain.Entities.User.Skills.Skill_Object
{
    /// <summary>
    /// Activity Metric object for Users' Activities.
    /// </summary>
    public class Activity
    {
        /// <summary>
        /// Gets or sets the Metric.
        /// </summary>
        public string Metric { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Kills.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the Rank.
        /// </summary>
        public int Rank { get; set; }
    }
}
