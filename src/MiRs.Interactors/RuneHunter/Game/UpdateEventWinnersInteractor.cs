using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.Discord;
using MiRs.Domain.Entities.Discord.Enums;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Logging;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.Game;
using MiRS.Gateway.DataAccess;
using MiRS.Gateway.DiscordBotClient;

namespace MiRs.Interactors.RuneHunter.Game
{
    public class UpdateEventWinnersInteractor : RequestHandler<UpdateEventWinnersRequest, UpdateEventWinnersResponse>
    {
        private readonly IGenericSQLRepository<GuildEvent> _guildEventRepository;
        private readonly IGenericSQLRepository<GuildCompletedEventArchive> _eventArchiveRepository;
        private readonly IGenericSQLRepository<GuildPermissions> _perms;

        private readonly ISender _mediator;

        private readonly AppSettings _appSettings;
        private readonly IDiscordBotClient _discordBotClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEventWinnersInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildEventRepository">The repo interface to SQL storage.</param>
        /// <param name="eventArchiveRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public UpdateEventWinnersInteractor(
            ILogger<ProcessUserLootInteractor> logger,
            IGenericSQLRepository<GuildEvent> guildEventRepository,
            IGenericSQLRepository<GuildCompletedEventArchive> eventArchiveRepository,
            IGenericSQLRepository<GuildPermissions> perms,
            IDiscordBotClient discordBotClient,
            ISender mediator,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _guildEventRepository = guildEventRepository;
            _eventArchiveRepository = eventArchiveRepository;
            _discordBotClient = discordBotClient;
            _mediator = mediator;
            _perms = perms;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to update event winners.
        /// </summary>
        /// <param name="request">The request to update event winners.</param>
        /// <param name="result"></param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        protected override async Task<UpdateEventWinnersResponse> HandleRequest(UpdateEventWinnersRequest request, UpdateEventWinnersResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.GameUpdateEventWinners, "Updating Event Team winners.");

            DateTimeOffset currentTimeUtc = DateTimeOffset.UtcNow;

            List<GuildEvent> gameEvents = (await _guildEventRepository.QueryWithInclude(e => e.EventActive, default,
                                                    ge => ge.Include(ge => ge.EventTeams).ThenInclude(et => et.CategoryProgresses).ThenInclude(cp => cp.CategoryLevelProcess).ThenInclude(cl => cl.LevelTaskProgress)
                                                              .Include(ge => ge.EventTeams).ThenInclude(et => et.CategoryProgresses).ThenInclude(c => c.Category)
                                                              .Include(ge => ge.EventTeams).ThenInclude(t => t.Team))).ToList();

            List<GuildEvent> expiredGameEvents = gameEvents.Where(ge => ge.EventEnd >= currentTimeUtc.AddMinutes(-5) && ge.EventEnd <= currentTimeUtc).ToList();

            List<GuildEvent> allActiveEvents = gameEvents.Where(ae => ae.EventEnd >= currentTimeUtc).ToList();

            foreach (GuildEvent ge in expiredGameEvents)
            {
                // Split off perm logic later to its own handler
                //GuildPermissionsResponse perm = await _mediator.Send(new GuildPermissionsRequest { GuildId = ge.GuildId, permissionType = Domain.Entities.Discord.Enums.PermissionType.Admin });
                GuildPermissions perm = (await _perms.Query(p => p.GuildId == ge.GuildId && p.Type == PermissionType.Admin)).FirstOrDefault();

                GuildTeam winningTeam = await GetWinningEventTeamForExpired(ge);

                await _discordBotClient.SendEventWinningTeam(winningTeam, perm);

                IList<int> teamIds = ge.EventTeams.Select(et => et.TeamId).ToList();

                IList<GuildPermissions> perms = (await _perms.Query(p => p.TeamId.HasValue && teamIds.Contains(p.TeamId.Value))).ToList();

                await _perms.DeleteManyAsync(perms);
            }

            foreach (GuildEvent ae in allActiveEvents)
            {
                // Split off perm logic later to its own handler
                //GuildPermissionsResponse perms = await _mediator.Send(new GuildPermissionsRequest { GuildId = ae.GuildId });

                GuildPermissions perm = (await _perms.Query(p => p.GuildId == ae.GuildId && p.Type == PermissionType.Admin)).FirstOrDefault();

                GuildTeam? winningTeam = await GetWinningEventTeamForActive(ae);

                if (winningTeam != null)
                {
                    await _discordBotClient.SendEventWinningTeam(winningTeam, perm);

                    IList<int> teamIds = ae.EventTeams.Select(et => et.TeamId).ToList();

                    IList<GuildPermissions> perms = (await _perms.Query(p => p.TeamId.HasValue && teamIds.Contains(p.TeamId.Value))).ToList();

                    await _perms.DeleteManyAsync(perms);

                }
            }

            return result;
        }

        private async Task<GuildTeam> GetWinningEventTeamForExpired(GuildEvent guildEvent)
        {

            Dictionary<GuildTeam, int> teamToPoints = new Dictionary<GuildTeam, int>();

            foreach (GuildEventTeam eventTeam in guildEvent.EventTeams)
            {
                teamToPoints.Add(eventTeam.Team, CalculateTeamPoints(eventTeam));
            }

            guildEvent.EventActive = false;
            GuildTeam winningTeam = teamToPoints.OrderByDescending(kvp => kvp.Value).FirstOrDefault().Key;

            await _eventArchiveRepository.AddAsync(new GuildCompletedEventArchive
            {
                GuildId = guildEvent.GuildId,
                Eventname = guildEvent.Eventname,
                EventComplete = true,
                CreatedDate = DateTime.UtcNow,
                EventStart = guildEvent.EventStart,
                EventEnd = guildEvent.EventEnd,
                EventTeamWinner = winningTeam.TeamName
            });

            return winningTeam;

        }

        private int CalculateTeamPoints(GuildEventTeam eventTeam)
        {
            int totalPoints = 0;
            GuildTeamCategoryProgress hubBase = eventTeam.CategoryProgresses.FirstOrDefault(c => c.Category.Domain.ToLower() == "master");

            if (hubBase == null)
            {
                return 0;
            }

            totalPoints += (hubBase.CategoryLevelProcess.Count(lp => lp.IsComplete)) * 100;
            totalPoints += (hubBase.CategoryLevelProcess.SelectMany(lt => lt.LevelTaskProgress).Count(lt => lt.IsComplete)) * 10;

            IList<GuildTeamCategoryProgress> pvm = eventTeam.CategoryProgresses.Where(c => c.Category.Domain.ToLower() == "pvm").ToList();

            foreach (GuildTeamCategoryProgress hp in pvm)
            {
                totalPoints += (hp.CategoryLevelProcess.Count(lp => lp.IsComplete)) * 30;
                totalPoints += (hp.CategoryLevelProcess.SelectMany(lt => lt.LevelTaskProgress).Count(lt => lt.IsComplete)) * 3;
            }

            IList<GuildTeamCategoryProgress> skilling = eventTeam.CategoryProgresses.Where(c => c.Category.Domain.ToLower() == "skilling").ToList();

            foreach (GuildTeamCategoryProgress skill in skilling)
            {
                totalPoints += (skill.CategoryLevelProcess.Count(lp => lp.IsComplete)) * 10;
                totalPoints += (skill.CategoryLevelProcess.SelectMany(lt => lt.LevelTaskProgress).Count(lt => lt.IsComplete)) * 1;
            }

            return totalPoints;
        }

        private async Task<GuildTeam?> GetWinningEventTeamForActive(GuildEvent guildEvent)
        {

            GuildTeam winingTeam;

            foreach (GuildEventTeam eventTeam in guildEvent.EventTeams)
            {
                if (eventTeam.CategoryProgresses.All(cp => cp.IsComplete))
                {

                    guildEvent.EventActive = false;

                    await _eventArchiveRepository.AddAsync(new GuildCompletedEventArchive
                    {
                        GuildId = guildEvent.GuildId,
                        Eventname = guildEvent.Eventname,
                        EventComplete = true,
                        CreatedDate = DateTime.UtcNow,
                        EventStart = guildEvent.EventStart,
                        EventEnd = guildEvent.EventEnd,
                        EventTeamWinner = eventTeam.Team.TeamName
                    });

                    return eventTeam.Team;
                }
            }

            return null;

        }

    }
}
