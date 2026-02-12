using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MiRs.Domain.Exceptions;
using MiRs.Mediator.Models.Discord;
using System.Net;

namespace MiRs.API.Controllers.RuneHunter
{
    /// <summary>
    /// This controller contains any calls relating to event management.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class GenController : ApiControllerBase
    {
        /// <summary>
        /// Get Guild Permissions
        /// </summary>
        /// <param name="guildId">the guild id.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("guildperms")]
        public async Task<IActionResult> GetGuildPermissions(ulong guildId)
        {
            try
            {
                return Ok(await Mediator.Send(new GuildPermissionsRequest { GuildId = guildId }));
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
        /// Api Connection Check
        /// </summary>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("ping")]
        public async Task<IActionResult> PingConnection()
        {
            try
            {
                return Ok(true);
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
