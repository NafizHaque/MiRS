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
        /// Get All Guild Teams. 
        /// </summary>
        /// <param name="guildId">The discord server Id.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(GuildTeam), StatusCodes.Status200OK)]
        [HttpGet("guilds")]
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
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(GuildTeam), StatusCodes.Status200OK)]
        [HttpPost("guilds")]
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
        /// Edit Guild Team
        /// </summary>
        /// <param name="guildId">The Guild Id.</param>
        /// <param name="teamId">The Team Id.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(GuildTeam), StatusCodes.Status200OK)]
        [HttpPatch("guilds")]
        public async Task<IActionResult> EditTeamInGuild(ulong guildId, string teamId)
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
        /// Get All Guild Events. 
        /// </summary>
        /// <param name="guildId">The discord server Id.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(GuildTeam), StatusCodes.Status200OK)]
        [HttpGet("events")]
        public async Task<IActionResult> GetEventsInGuild(ulong guildId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetGuildEventsRequest { GuildId = guildId }));

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
        /// Create Guild Event.
        /// </summary>
        /// <param name="guildEvent">The discord event object.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return .</remarks>
        [ProducesResponseType(typeof(GuildEvent), StatusCodes.Status200OK)]
        [HttpPost("events")]
        public async Task<IActionResult> CreateEventInGuild([FromBody] GuildEvent guildEvent)
        {
            try
            {
                return Ok(await Mediator.Send(new CreateEventInGuildRequest { GuildEventToBeCreated = guildEvent }));

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
        /// Placeholder Text. 
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
        /// Placeholder Text. 
        /// </summary>
        /// <param name="username">Test.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpPost]
        [Route("EventTeam")]
        public async Task<IActionResult> AddGuildTeamToEvent(int teamid, int eventid)
        {
            try
            {
                return Ok(await Mediator.Send(new AddGuildTeamToEventRequest { TeamId = teamid, EventId = eventid }));

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
        /// Test Api Connection
        /// </summary>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
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
