namespace MiRs.Domain.Entities.User
{

    /// <summary>
    /// User record that holds data about the runescape character and metrics.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the DisplayName.
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the GameMode.
        /// </summary>
        public string GameMode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Build.
        /// </summary>
        public string Build { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public string status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Patron.
        /// </summary>
        public bool Patron { get; set; }

        /// <summary>
        /// Gets or sets the TotalExperiance.
        /// </summary>
        public long TotalExperiance { get; set; }

        /// <summary>
        /// Gets or sets the Ehp.
        /// </summary>
        public decimal Ehp { get; set; }

        /// <summary>
        /// Gets or sets the Ehb.
        /// </summary>
        public decimal Ehb { get; set; }

        /// <summary>
        /// Gets or sets the ttm.
        /// </summary>
        public decimal TimeToMax { get; set; }

        /// <summary>
        /// Gets or sets the ttm200.
        /// </summary>
        public decimal TimeToMax200m { get; set; }

        /// <summary>
        /// Gets or sets the RegisteredAt.
        /// </summary>
        public DateTimeOffset? RegisteredAt { get; set; }


        /// <summary>
        /// Gets or sets the RegisteredAt.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; set; }


        /// <summary>
        /// Gets or sets the RegisteredAt.
        /// </summary>
        public DateTimeOffset? LastChangedAt { get; set; }

        /// <summary>
        /// Gets or sets the RegisteredAt.
        /// </summary>
        public DateTimeOffset? LastImportedAt { get; set; }

        /// <summary>
        /// Gets or sets the CombatLevel.
        /// </summary>
        public int CombatLevel { get; set; }

        /// <summary>
        /// Gets or sets the Annotations.
        /// </summary>
        //public UserAnnotations? Annotations { get; set; } = new UserAnnotations();

        /// <summary>
        /// Gets or sets the LatestSnapshot.
        /// </summary>
        public UserLatestSnapshot? LatestSnapshot { get; set; } = new UserLatestSnapshot();

    }
}
