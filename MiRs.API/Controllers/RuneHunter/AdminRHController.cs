using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Entities.User;
using MiRs.Domain.Exceptions;
using MiRs.Mediator.Models.RuneHunter;
using MiRs.Mediator.Models.RuneUser;
using System.Net;

namespace MiRs.API.Controllers.RuneHunter
{
    public class AdminRHController : ApiControllerBase
    {
        private readonly ILogger<AdminRHController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RHUserController"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        public AdminRHController(ILogger<AdminRHController> logger) => _logger = logger;

        /// <summary>
        /// User to Join a Team
        /// </summary>
        /// <param name="username">Test.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> CreateTeamInGuild(int guildId, string teamname)
        {
            try
            {
                return Ok(await Mediator.Send(new JoinTeamRequest { UserId = guildId, Teamname = teamname }));

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
        /// User to Join a Team
        /// </summary>
        /// <param name="username">Test.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> EditTeamInGuild(int guildId, string teamname)
        {
            try
            {
                return Ok(await Mediator.Send(new JoinTeamRequest { UserId = guildId, Teamname = teamname }));

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
        /// User to Join a Team
        /// </summary>
        /// <param name="username">Test.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> DeleteTeamInGuild(int guildId, string teamname)
        {
            try
            {
                return Ok(await Mediator.Send(new JoinTeamRequest { UserId = guildId, Teamname = teamname }));

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
        /// User to Join a Team
        /// </summary>
        /// <param name="username">Test.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> ChangeUserTeamInGuild(int id, int guildId, string teamname)
        {
            try
            {
                return Ok(await Mediator.Send(new JoinTeamRequest { UserId = id, Teamname = teamname }));

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
        /// User to Join a Team
        /// </summary>
        /// <param name="username">Test.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> RemoveUserTeamInGuild(int id, int guildId, string teamname)
        {
            try
            {
                return Ok(await Mediator.Send(new JoinTeamRequest { UserId = id, Teamname = teamname }));

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
