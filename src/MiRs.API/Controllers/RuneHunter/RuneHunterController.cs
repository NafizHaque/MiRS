using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MiRs.API.ApiDTOs;
using MiRs.Domain.DTOs.RuneHunter;
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
        /// Get Game Metadata
        /// </summary>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("metadata")]
        public async Task<IActionResult> GetGameMetadata()
        {
            try
            {
                return Ok(await Mediator.Send(new GetGameMetadataRequest()));
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
        /// Update Game Metadata
        /// </summary>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpPost]
        [Route("metadata")]
        public async Task<IActionResult> UpdateGameMetadata([FromBody] IEnumerable<CategoryDto> cat)
        {
            try
            {
                return Ok(await Mediator.Send(new UpdateGameMetadataRequest { Categories = cat }));
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
        /// Register User Loot
        /// </summary>
        /// <param name="userId">userid.</param>
        /// <param name="guildId">guildid.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpGet("progress")]
        public async Task<IActionResult> GetEventTeamProgressForUser(ulong userId, ulong guildId)
        {
            try
            {

                return Ok(await Mediator.Send(new GetEventTeamProgressForUserRequest { UserId = userId, GuildId = guildId }));

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
        /// Get Team Loot
        /// </summary>
        /// <param name="userId">userid.</param>
        /// <param name="guildId">guildid.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return Team Loot.</remarks>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpGet("loot")]
        public async Task<IActionResult> GetLatestTeamLoot(ulong userId, ulong guildId)
        {
            try
            {

                return Ok(await Mediator.Send(new GetRecentTeamLootRequest { UserId = userId, GuildId = guildId }));

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
