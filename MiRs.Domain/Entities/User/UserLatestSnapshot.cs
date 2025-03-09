using System.Text.Json.Serialization;

namespace MiRs.Domain.Entities.User
{
    /// <summary>
    /// This is the Users' latest datapoint containing their user metrics
    /// </summary>
    public class UserLatestSnapshot
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int PlayerID { get; set; }

        /// <summary>
        /// Gets or sets the CreatedAt.
        /// </summary>
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the CreatedAt.
        /// </summary>
        public DateTimeOffset? ImportedAt { get; set; }

        /// <summary>
        /// Gets or sets the CreatedAt.
        /// </summary>
        [JsonPropertyName("data")]
        public UserMetrics UserMetrics { get; set; } = new UserMetrics();
    }
}
