using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.DTOs.RuneHunter;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;
using MiRs.Domain.Logging;
using MiRs.Domain.Mappers;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.Game;
using MiRS.Gateway.DataAccess;

namespace MiRs.Interactors.RuneHunter.Game
{
    public class GetEventTeamProgressForUserInteractor : RequestHandler<GetEventTeamProgressForUserRequest, GetEventTeamProgressForUserResponse>
    {
        private readonly IGenericSQLRepository<GuildEvent> _guildevent;
        private readonly IGenericSQLRepository<RHUserToTeam> _userToTeam;

        private readonly AppSettings _appSettings;
        private readonly GameMapper _gameMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetEventTeamProgressForUserInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildevent">The repo interface to SQL storage.</param>
        /// <param name="userToTeam">The repo interface to SQL storage.</param>
        /// <param name="mapper">The mapper for entity to DTO .</param>
        /// <param name="appSettings">The app settings.</param>
        public GetEventTeamProgressForUserInteractor(
            ILogger<ProcessUserLootInteractor> logger,
            IGenericSQLRepository<GuildEvent> guildevent,
            IGenericSQLRepository<RHUserToTeam> userToTeam,
            GameMapper mapper,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _guildevent = guildevent;
            _userToTeam = userToTeam;
            _gameMapper = mapper;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to update game state.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<GetEventTeamProgressForUserResponse> HandleRequest(GetEventTeamProgressForUserRequest request, GetEventTeamProgressForUserResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.GetEventTeamProgress, "Retrieving event team progress by user: {userid} and guild {guildid}.", request.UserId, request.GuildId);

            IList<GuildEvent> activeGuildEvents = (await _guildevent.QueryWithInclude(ge => ge.GuildId == request.GuildId && ge.EventActive == true, default,
                                                                ge => ge.Include(ge => ge.EventTeams)
                                                                            .ThenInclude(et => et.CategoryProgresses)
                                                                                .ThenInclude(cp => cp.CategoryLevelProcess)
                                                                                    .ThenInclude(clp => clp.LevelTaskProgress)
                                                                                        .ThenInclude(ltp => ltp.LevelTask)
                                                                        .Include(ge => ge.EventTeams)
                                                                            .ThenInclude(et => et.CategoryProgresses)
                                                                                .ThenInclude(cp => cp.Category)
                                                                        .Include(ge => ge.EventTeams)
                                                                        .ThenInclude(t => t.Team)
                                                                        .Include(ge => ge.EventTeams)
                                                                            .ThenInclude(et => et.CategoryProgresses)
                                                                                .ThenInclude(cp => cp.CategoryLevelProcess)
                                                                                    .ThenInclude(clp => clp.Level))).ToList();

            IList<GuildTeamCategoryProgress> progresses;

            if (!activeGuildEvents.Any() || !activeGuildEvents.SelectMany(ge => ge.EventTeams).Any())
            {
                throw new BadRequestException("Cannot find any current events or teams in guild");
            }

            IList<RHUserToTeam> userTeams = (await _userToTeam.Query(ge => ge.UserId == request.UserId)).ToList();

            foreach (GuildEvent activeGuildEvent in activeGuildEvents)
            {
                IEnumerable<GuildEventTeam> teamToevent = activeGuildEvent.EventTeams.Where(et => userTeams.Any(ut => ut.TeamId == et.TeamId));

                foreach (GuildEventTeam guildEventTeam in teamToevent)
                {
                    result.EventTeamProgresses.Add(new EventTeam
                    {
                        Id = guildEventTeam.Id,
                        TeamId = guildEventTeam.TeamId,
                        Team = new GameTeam { TeamName = guildEventTeam.Team.TeamName },
                        EventId = guildEventTeam.EventId,
                        CategoryProgresses = guildEventTeam.CategoryProgresses?
                            .Select(_gameMapper.Map)
                            .ToList() ?? new List<CategoryProgress>()
                    });
                }

            }

            return result;
        }
    }
}
