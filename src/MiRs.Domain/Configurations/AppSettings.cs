namespace MiRs.Domain.Configurations
{
    /// <summary>
    /// Class that represents app setting from Azure config.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or Sets the Test.
        /// </summary>
        public string Test { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets the Test.
        /// </summary>
        public string PasswordSalt { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets the DiscordBotUrl.
        /// </summary>
        public string DiscordBotDomain { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets the BlacklistedSources.
        /// </summary>
        public IEnumerable<string> BlacklistedSources { get; set; } = Enumerable.Empty<string>();


        /// <summary>
        /// Gets or Sets the WhitelistedSources.
        /// </summary>
        public IEnumerable<string> WhitelistedSources { get; set; } = Enumerable.Empty<string>();
    }
}
