using System.Net;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MiRs.Domain.Entities.User;
using MiRs.Domain.Exceptions;
using MiRs.Mediator.Models.RuneUser;

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
        /// Get users call
        /// </summary>
        /// <param name="username">Test.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetUserStats(string username)
        {
            try
            {
               return Ok( await Mediator.Send(new GetRuneUserRequest { Username = username }));

            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.CustomErrorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Sends a Request to the service provider to update User
        /// </summary>
        /// <param name="username">Test.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> UpdateUserStats(string username)
        {
            try
            {
                return Ok(await Mediator.Send(new GetRuneUserRequest { Username = username }));

            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.CustomErrorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
