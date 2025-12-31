using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Logging;
using MiRS.Gateway.DataAccess;
using MiRs.Mediator.Models.RuneHunter.Admin;
using MiRs.Mediator;
using MiRs.Domain.Exceptions;

namespace MiRs.Interactors.RuneHunter.Admin
{
    public class AddGuildTeamToEventInteractor : RequestHandler<AddGuildTeamToEventRequest, AddGuildTeamToEventResponse>
    {
        private readonly IGenericSQLRepository<GuildEventTeam> _guildTeamEventRepository;
        private readonly IGenericSQLRepository<GuildTeam> _guildTeamRepository;
        private readonly IGenericSQLRepository<GuildEvent> _guildEventRepository;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddGuildTeamToEventInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public AddGuildTeamToEventInteractor(
            ILogger<AddGuildTeamToEventInteractor> logger,
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
        /// Handles the request to create a Guild team.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<AddGuildTeamToEventResponse> HandleRequest(AddGuildTeamToEventRequest request, AddGuildTeamToEventResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.CreateGuildTeam, "Linking Team to Event. Team Id: {teamId}, Event Id: {eventId} ", request.TeamId, request.EventId);

            if (!(await _guildTeamRepository.Query(t => t.Id == request.TeamId)).Any())
            {
                throw new BadRequestException($"Team: {request.TeamId} is not in guild!");
            }

            if (!(await _guildEventRepository.Query(t => t.Id == request.EventId)).Any())
            {
                throw new BadRequestException($"Event: {request.EventId} is not in guild!");
            }

            if ((await _guildTeamEventRepository.Query(t => t.EventId == request.EventId && t.TeamId == request.TeamId)).Any())
            {
                throw new BadRequestException($"Team: {request.TeamId} is already registered to Event: {request.EventId}");
            }

            await _guildTeamEventRepository.AddAsync(
                new GuildEventTeam
                {
                    TeamId = request.TeamId,
                    EventId = request.EventId,
                });

            return result;
        }
    }
}
