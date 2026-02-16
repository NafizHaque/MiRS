using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;
using MiRs.Mediator.Models.RuneHunter.User;
using System.Net;

namespace MiRs.API.Controllers.RuneHunter
{
    /// <summary>
    /// This controller contains any calls relating to users.
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UserController : ApiControllerBase
    {
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        public UserController(ILogger<UserController> logger) => _logger = logger;

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="rhUser">User object details.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RHUser rhUser)
        {
            try
            {
                return Ok(await Mediator.Send(new RegisterUserRequest { rhUserToBeCreated = rhUser }));

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
        /// User Search
        /// </summary>
        /// <param name="search">the search term.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return users.</remarks>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> UserSearch(string search, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await Mediator.Send(new UserSearchRequest { Searchkey = search }, cancellationToken));

            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.CustomErrorMessage);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request Aborted");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get current Events for User
        /// </summary>
        /// <param name="userid">Test.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("CurrentUserEvents")]
        public async Task<IActionResult> CurrentEventsForUser(ulong userid)
        {
            try
            {
                return Ok(await Mediator.Send(new GetCurrentEventsForUserRequest { UserId = userid }));

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
