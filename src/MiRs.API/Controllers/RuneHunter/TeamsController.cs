using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;
using MiRs.Mediator.Models.RuneHunter.Admin.Team;
using System.Net;

namespace MiRs.API.Controllers.RuneHunter
{
    /// <summary>
    /// This controller contains any calls relating to Team management.
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class TeamsController : ApiControllerBase
    {
        private readonly ILogger<TeamsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        public TeamsController(ILogger<TeamsController> logger) => _logger = logger;

        /// <summary>
        /// Get All Guild Teams. 
        /// </summary>
        /// <param name="guildId">The discord server Id.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [ProducesResponseType(typeof(GuildTeam), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetTeamsInGuild(ulong guildId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetGuildTeamsRequest { GuildId = guildId }));

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
        /// Create Guild Team
        /// </summary>
        /// <param name="guildId">The discord server Id.</param>
        /// <param name="teamname">The Team name.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [ProducesResponseType(typeof(GuildTeam), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> CreateTeamInGuild(ulong guildId, string teamname)
        {
            try

            {
                return Ok(await Mediator.Send(new CreateGuildTeamRequest { GuildId = guildId, Teamname = teamname }));

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
        /// Update Guild Team 
        /// </summary>
        /// <param name="guildTeam">The Guild Team to be updated.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [ProducesResponseType(typeof(GuildTeam), StatusCodes.Status200OK)]
        [HttpPatch]
        public async Task<IActionResult> UpdateTeamInGuild([FromBody] GuildTeam guildTeam)
        {
            try
            {
                return Ok(await Mediator.Send(new UpdateGuildTeamRequest { TeamToBeUpdated = guildTeam }));

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
