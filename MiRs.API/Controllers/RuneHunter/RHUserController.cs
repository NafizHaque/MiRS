﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Entities.User;
using MiRs.Domain.Exceptions;
using MiRs.Mediator.Models.RuneHunter;
using MiRs.Mediator.Models.RuneUser;
using System.Net;

namespace MiRs.API.Controllers.RuneHunter
{
    /// <summary>
    /// This controller contains any calls relating to users.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class RHUserController : ApiControllerBase
    {
        private readonly ILogger<RHUserController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RHUserController"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        public RHUserController(ILogger<RHUserController> logger) => _logger = logger;

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="username">Test.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(RHUser), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RHUser rhUser)
        {
            try
            {
                return Ok(await Mediator.Send(new RegisterUserRequest { rhUser = rhUser }));

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
        public async Task<IActionResult> JoinTeam(int id, string teamname)
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
        public async Task<IActionResult> GetUsersInTeam(int id, string teamname)
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
        public async Task<IActionResult> GetTeamsInGuild(int id, string teamname)
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
