using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MiRs.Domain.DTOs.RuneHunter;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;
using MiRs.Mediator.Models.RuneHunter.Admin.Event;
using System.Net;

namespace MiRs.API.Controllers.RuneHunter
{
    /// <summary>
    /// This controller contains any calls relating to event management.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class EventsController : ApiControllerBase
    {
        /// <summary>
        /// Get All Guild Events. 
        /// </summary>
        /// <param name="guildId">The discord server Id.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [ProducesResponseType(typeof(GuildTeam), StatusCodes.Status200OK)]
        [HttpGet]
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
        /// Get All Events. 
        /// </summary>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [ProducesResponseType(typeof(GuildEvent), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("allevents")]
        public async Task<IActionResult> GetAllEvents()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllEventsRequest()));

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
        [ProducesResponseType(typeof(GuildEvent), StatusCodes.Status200OK)]
        [HttpPost]
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
        /// Get Guild Team from Event
        /// </summary>
        /// <param name="eventid">the event id.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("teamstoevent")]
        public async Task<IActionResult> GetGuildTeamsFromEvent(int eventid)
        {
            try
            {
                return Ok(await Mediator.Send(new GetGuildTeamFromEventRequest { EventId = eventid }));
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
        /// Add Guild Team to Event
        /// </summary>
        /// <param name="teamid">the event team Id.</param>
        /// <param name="eventid">the event id.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpPost]
        [Route("teamstoevent")]
        public async Task<IActionResult> AddGuildTeamToEvent(int teamid, int eventid)
        {
            try
            {
                return Ok(await Mediator.Send(new AddGuildTeamToEventRequest { TeamId = teamid, EventId = eventid }));
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
        /// Update Guild Team to Event
        /// </summary>
        /// <param name="updateTeamList">update team list that contains the new team and the current team list.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpPatch]
        [Route("teamstoevent")]
        public async Task<IActionResult> UpdateTeamsToEvent([FromBody] UpdateTeamList updateTeamList)
        {
            try
            {
                return Ok(await Mediator.Send(new UpdateTeamsToEventRequest
                {
                    EventId = updateTeamList.EventId,
                    AddExistingTeamToggle = updateTeamList.AddExistingTeamToggle,
                    NewTeamToBeCreated = updateTeamList.NewTeamToBeCreated,
                    CurrentTeamsToBeUpdated = updateTeamList.CurrentTeamsToBeUpdated,
                }));
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
        /// Unlink Guild Team From Event
        /// </summary>
        /// <param name="teamid">the event team Id.</param>
        /// <param name="eventid">the event id.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpDelete]
        [Route("teamstoevent")]
        public async Task<IActionResult> RemoveTeamFromEvent(int teamid, int eventid)
        {
            try
            {
                return Ok(await Mediator.Send(new RemoveTeamFromEventRequest { TeamId = teamid, EventId = eventid }));
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
        /// Add Guild Team to Event
        /// </summary>
        /// <param name="eventid">the event id.</param>
        /// <param name="guildid">the guild id.</param>
        /// <param name="eventpassword">the event password.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("verify")]
        public async Task<IActionResult> UpdateEventVerification(int eventid, ulong guildid, string eventpassword)
        {
            try
            {
                return Ok(await Mediator.Send(new UpdateEventVerificationRequest { EventId = eventid, GuildId = guildid, EventPassword = eventpassword }));
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
