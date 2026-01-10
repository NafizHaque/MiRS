using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MiRs.API.ApiDTOs;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;
using MiRs.Mediator.Models.RuneHunter.Game;
using System.Net;

namespace MiRs.API.Controllers.RuneHunter
{
    /// <summary>
    /// This controller contains any calls relating to users.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class RuneHunterController : ApiControllerBase
    {
        private readonly ILogger<RuneHunterController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        public RuneHunterController(ILogger<RuneHunterController> logger) => _logger = logger;

        /// <summary>
        /// Register User Loot
        /// </summary>
        /// <param name="username">Test.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpPost("loot")]
        public async Task<IActionResult> UserLootLogging([FromBody] RuneLoot loot)
        {
            try
            {

                return Ok(await Mediator.Send(new LogUserLootRequest { LootMessage = loot.Content }));

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
        /// /Initialise team progress
        /// </summary>
        /// <param name="teamid">teamid.</param>
        /// <param name="eventid">eventid.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpPost]
        [Route("InitTeamProgress")]
        public async Task<IActionResult> InitTeamProgress(int teamid, int eventid)
        {
            try
            {
                return Ok(await Mediator.Send(new InitaliseTeamProgressRequest { TeamId = teamid, EventId = eventid }));

                throw new NotImplementedException();

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
