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

    }
}
