using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;
using MiRs.Domain.Logging;
using MiRS.Gateway.DataAccess;
using MiRs.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiRs.Mediator.Models.RuneHunter.User;

namespace MiRs.Interactors.RuneHunter.User
{
    internal class JoinTeamInteractor : RequestHandler<JoinTeamRequest, JoinTeamResponse>
    {
        private readonly IGenericSQLRepository<RHUserToTeam> _rhUserToTeamRepository;
        private readonly IGenericSQLRepository<GuildTeam> _guildTeamRepository;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUserInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="userRepository">The repo interface to table storage.</param>
        /// <param name="configRepository">The repo interface to table storage for config data.</param>
        /// <param name="appSettings">The app settings.</param>
        public JoinTeamInteractor(
            ILogger<RegisterUserInteractor> logger,
            IGenericSQLRepository<RHUserToTeam> rhUserToTeamRepository,
            IGenericSQLRepository<GuildTeam> guildTeamRepository,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _rhUserToTeamRepository = rhUserToTeamRepository;
            _guildTeamRepository = guildTeamRepository;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to create a user.
        /// </summary>
        /// <param name="request">The request to create user.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<JoinTeamResponse> HandleRequest(JoinTeamRequest request, JoinTeamResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.UserToTeamJoin, "User joining team. User Id: {userId}", request.UserId);

            IEnumerable<RHUserToTeam> userToTeamsinTable = await _rhUserToTeamRepository.GetAllEntitiesAsync(
                  null, default, utt => utt.User, utt => utt.Team);

            if (userToTeamsinTable.Any(u => u.Team!.GuildId == request.GuildId && u.Team!.TeamName == request.Teamname))
            {
                throw new BadRequestException($"User: <@{request.UserId}> Already In Team!");
            
            }

            RHUserToTeam userToTeamToCreate = new RHUserToTeam
            {
                UserId = request.UserId,
                TeamId = (await _guildTeamRepository.Query(t => t.TeamName == request.Teamname)).Select(t => t.Id).FirstOrDefault(),
            };

            await _rhUserToTeamRepository.AddAsync(userToTeamToCreate);
            return result;

        }
    }
}
