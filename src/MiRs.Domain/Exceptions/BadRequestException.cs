namespace MiRs.Domain.Exceptions
{
    /// <summary>
    /// This custom exception type is used when a bad request is sent to an interactor.
    /// </summary>
    public class BadRequestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class.
        /// </summary>
        /// <param name="errorMessage">The custom error message for the bad request.</param>
        public BadRequestException(string errorMessage)
        {
            CustomErrorMessage = errorMessage;
        }

        /// <summary>
        /// Gets or sets the custom error message.
        /// </summary>
        public string? CustomErrorMessage { get; set; }
    }
}
