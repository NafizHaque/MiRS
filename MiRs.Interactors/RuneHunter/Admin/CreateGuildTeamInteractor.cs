﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Logging;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter;
using MiRs.Mediator.Models.RuneHunter.Admin;
using MiRS.Gateway.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Interactors.RuneHunter.Admin
{
    public class CreateGuildTeamInteractor : RequestHandler<CreateGuildTeamRequest, CreateGuildTeamResponse>
    {
        private readonly IGenericSQLRepository<GuildTeam> _guildTeamRepository;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGuildTeamInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public CreateGuildTeamInteractor(
            ILogger<CreateGuildTeamInteractor> logger,
            IGenericSQLRepository<GuildTeam> guildTeamRepository,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _guildTeamRepository = guildTeamRepository;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to create a Guild team.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<CreateGuildTeamResponse> HandleRequest(CreateGuildTeamRequest request, CreateGuildTeamResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.CreateGuildTeam, "Creating Guild Team. Guild Id: {guildId}, Teamname: {teamname}", request.GuildId, request.Teamname);

            await _guildTeamRepository.AddAsync(
                new GuildTeam
                {
                    GuildId = request.GuildId,
                    TeamName = request.Teamname,
                });

            return result;
        }
    }
}
