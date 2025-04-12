using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Entities.User;
using MiRs.Domain.Exceptions;
using MiRs.Mediator.Models.RuneHunter;
using MiRs.Mediator.Models.RuneHunter.Admin;
using MiRs.Mediator.Models.RuneUser;
using System.Net;

namespace MiRs.API.Controllers.RuneHunter
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
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
        /// <param name="guildId">The discord server Id.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(GuildTeam), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetTeamsInGuild(int guildId)
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
        /// User to Join a Team
        /// </summary>
        /// <param name="guildId">The discord server Id.</param>
        /// <param name="teamname">The Team name.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(GuildTeam), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> CreateTeamInGuild(int guildId, string teamname)
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
        /// User to Join a Team
        /// </summary>
        /// <param name="guildId">The Guild Id.</param>
        /// <param name="teamId">The Team Id.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(GuildTeam), StatusCodes.Status200OK)]
        [HttpPatch]
        public async Task<IActionResult> EditTeamInGuild(int guildId, string teamId)
        {
            try
            {
                //return Ok(await Mediator.Send(new JoinTeamRequest { UserId = guildId, Teamname = teamname }));
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

        /// <summary>
        /// User to Join a Team
        /// </summary>
        /// <param name="guildId">Test.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(GuildTeam), StatusCodes.Status200OK)]
        [HttpDelete]
        public async Task<IActionResult> DeleteTeamInGuild(int guildId, string teamId)
        {
            try
            {
                //return Ok(await Mediator.Send(new JoinTeamRequest { UserId = guildId, Teamname = teamname }));
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

        /// <summary>
        /// User to Join a Team
        /// </summary>
        /// <param name="username">Test.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpPost]
        [Route("UserTeam")]
        public async Task<IActionResult> AddUserToTeamInGuild(int userid, int guildId, string teamnId)
        {
            try
            {
                //return Ok(await Mediator.Send(new JoinTeamRequest { UserId = userid, Teamname = teamname }));

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

        /// <summary>
        /// User to Join a Team
        /// </summary>
        /// <param name="username">Test.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpPost]
        [Route("UserTeam")]
        public async Task<IActionResult> ChangeUserToTeamInGuild(int userid, int guildId, string teamId)
        {
            try
            {
                //return Ok(await Mediator.Send(new JoinTeamRequest { UserId = userid, Teamname = teamname }));

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

        /// <summary>
        /// User to Join a Team
        /// </summary>
        /// <param name="username">Test.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> RemoveUserToTeamInGuild(int userid, int guildId, string teamname)
        {
            try
            {
                //return Ok(await Mediator.Send(new JoinTeamRequest { UserId = id, Teamname = teamname }));

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
