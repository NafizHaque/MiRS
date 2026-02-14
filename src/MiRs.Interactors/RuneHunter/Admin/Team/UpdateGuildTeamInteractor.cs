using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;
using MiRs.Domain.Logging;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.Admin.Team;
using MiRS.Gateway.DataAccess;

namespace MiRs.Interactors.RuneHunter.Admin.Team
{
    public class UpdateGuildTeamInteractor : RequestHandler<UpdateGuildTeamRequest, UpdateGuildTeamResponse>
    {
        private readonly IGenericSQLRepository<GuildEventTeam> _guildTeamEventRepository;
        private readonly IGenericSQLRepository<GuildTeam> _guildTeamRepository;
        private readonly IGenericSQLRepository<GuildEvent> _guildEventRepository;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateGuildTeamInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public UpdateGuildTeamInteractor(
            ILogger<UpdateGuildTeamInteractor> logger,
            IGenericSQLRepository<GuildEventTeam> guildTeamEventRepository,
            IGenericSQLRepository<GuildTeam> guildTeamRepository,
            IGenericSQLRepository<GuildEvent> guildEventRepository,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _guildTeamEventRepository = guildTeamEventRepository;
            _guildTeamRepository = guildTeamRepository;
            _guildEventRepository = guildEventRepository;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to unlink Team from Event.
        /// </summary>
        /// <param name="request">The request to unlink team from event.</param>
        /// <param name="result"></param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        protected override async Task<UpdateGuildTeamResponse> HandleRequest(UpdateGuildTeamRequest request, UpdateGuildTeamResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.UpdateGuildTeam, "Updating Team details. Team Id: {teamId} ", request.TeamToBeUpdated.Id);

            GuildTeam entityToBeUpdated = (await _guildTeamRepository.Query(t => t.Id == request.TeamToBeUpdated.Id)).FirstOrDefault();

            if (entityToBeUpdated == null)
            {
                throw new BadRequestException($"Team: {request.TeamToBeUpdated.Id} could not be found!");
            }

            entityToBeUpdated.TeamName = request.TeamToBeUpdated.TeamName;

            await _guildTeamRepository.UpdateAsync(entityToBeUpdated);

            return result;
        }
    }
}
