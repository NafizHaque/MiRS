using System.Net;
using System.Security.Cryptography;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiRs.Interactors;

namespace MiRs.API.Controllers
{
    /// <summary>
    /// This controller contains any calls relating to users.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class RuneUserController : ApiControllerBase
    {
        private readonly ILogger<RuneUserController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        public RuneUserController(ILogger<RuneUserController> logger) => _logger = logger;

        /// <summary>
        /// Test Example Controller
        /// </summary>
        /// <param name="testNumber">Test.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return no content.</remarks>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public async Task<IActionResult> ExampleMethod(int testNumber)
        {
            _logger.LogDebug(1, "Executing ExampleMethod");
            return NoContent();
        }

        /// <summary>
        /// Test Example Controller
        /// </summary>
        /// <param name="testNumber">Test.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return no content.</remarks>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public async Task<IActionResult> GetUserStats(int testNumber)
        {
            _logger.LogDebug(1, "Executing ExampleMethod");
            return NoContent();
        }

    }
}
