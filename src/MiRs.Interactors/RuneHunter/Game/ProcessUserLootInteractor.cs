using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.DTOs.RuneHunter;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Entities.RuneHunterData;
using MiRs.Domain.Logging;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.Game;
using MiRs.Mediator.Models.RuneHunter.User;
using MiRS.Gateway.DataAccess;

namespace MiRs.Interactors.RuneHunter.Game
{
    public class ProcessUserLootInteractor : RequestHandler<ProcessUserLootRequest, ProcessUserLootResponse>
    {
        private readonly IGenericSQLRepository<RHUserRawLoot> _rhUserRawLoot;
        private readonly IGenericSQLRepository<RHUser> _rhUserRepository;
        private readonly IGenericSQLRepository<RunescapeLootAlias> _rhLootAlias;

        private readonly IGenericSQLRepository<Category> _category;
        private readonly IGenericSQLRepository<GuildTeamCategoryProgress> _categoryProgress;

        private readonly IGenericSQLRepository<Level> _level;
        private readonly IGenericSQLRepository<GuildTeamCategoryLevelProgress> _levelProgress;

        private readonly IGenericSQLRepository<GuildTeamLevelTaskProgress> _levelTaskProgress;
        private readonly IGenericSQLRepository<LevelTask> _levelTasks;

        private readonly AppSettings _appSettings;
        private readonly ISender _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessUserLootInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public ProcessUserLootInteractor(
            ILogger<ProcessUserLootInteractor> logger,
            IGenericSQLRepository<RHUserRawLoot> rhUserRawLoot,
            IGenericSQLRepository<RHUser> rhUserRepository,
            IGenericSQLRepository<LevelTask> levelTasks,
            IGenericSQLRepository<GuildTeamLevelTaskProgress> levelTaskProgress,
            IGenericSQLRepository<Level> level,
            IGenericSQLRepository<GuildTeamCategoryLevelProgress> levelProgress,
            IGenericSQLRepository<Category> category,
            IGenericSQLRepository<GuildTeamCategoryProgress> categoryProgress,
            IGenericSQLRepository<RunescapeLootAlias> rhLootAlias,
            ISender mediator,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _rhUserRawLoot = rhUserRawLoot;
            _rhUserRepository = rhUserRepository;
            _levelTasks = levelTasks;
            _levelTaskProgress = levelTaskProgress;
            _level = level;
            _levelProgress = levelProgress;
            _category = category;
            _categoryProgress = categoryProgress;
            _rhLootAlias = rhLootAlias;
            _appSettings = appSettings.Value;
            _mediator = mediator;
        }

        /// <summary>
        /// Handles the request to process user loot.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<ProcessUserLootResponse> HandleRequest(ProcessUserLootRequest request, ProcessUserLootResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.GameProcessLoot, "Proocessing User Loot.");

            IEnumerable<RHUserRawLoot> unprocessedUserLoot = await _rhUserRawLoot.Query(l => l.Processed == false);

            IEnumerable<RunescapeLootAlias> runescapeLootAlias = await _rhLootAlias.GetAllEntitiesAsync();

            IEnumerable<IGrouping<ulong, RHUserRawLoot>> groupedLoot = unprocessedUserLoot.GroupBy(l => l.UserId);
            try
            {
                foreach (IGrouping<ulong, RHUserRawLoot> group in groupedLoot)
                {
                    GetCurrentEventsForUserResponse currentUserEvents = await _mediator.Send(new GetCurrentEventsForUserRequest { UserId = group.Key });

                    group.OrderBy(g => g.DateLogged);

                    foreach (RHUserRawLoot loot in group)
                    {
                        foreach (UserEvents userEvent in currentUserEvents.UserCurrentEvents)
                        {
                            await AssignUserLootToTeams(loot, runescapeLootAlias, userEvent);
                        }

                        loot.Processed = true;

                        await _rhUserRawLoot.UpdateAsync(loot);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError((int)LoggingEvents.GameProcessLoot, ex.Message);
            }

            Logger.LogInformation((int)LoggingEvents.GameProcessLoot, "Loot ({totalLoot}) to be processed.", unprocessedUserLoot.Count());

            return result;
        }

        private async Task AssignUserLootToTeams(RHUserRawLoot loot, IEnumerable<RunescapeLootAlias> runescapeLootAlias, UserEvents userEvent)
        {
            IList<GuildTeamLevelTaskProgress> levelTasksProgress = (await _levelTaskProgress.GetAllEntitiesAsync(t => t.IsComplete == false && t.GuildEventTeamId == userEvent.Id, default, lt => lt.Include(ltt => ltt.LevelTask))).ToList();

            if (!levelTasksProgress.Any())
            {
                return;
            }

            int minLevel = levelTasksProgress.Min(t => t.LevelTask.LevelId);

            List<GuildTeamLevelTaskProgress> lowestIncompleteTasks = levelTasksProgress
                .Where(t => t.LevelTask.Levelnumber == minLevel)
                .ToList();

            foreach (GuildTeamLevelTaskProgress taskProgress in lowestIncompleteTasks)
            {
                if (string.Equals(taskProgress.LevelTask.Name, loot.Loot, StringComparison.OrdinalIgnoreCase))
                {
                    taskProgress.Progress += loot.Quantity;

                    if (taskProgress.Progress > taskProgress.LevelTask.Goal)
                    {
                        taskProgress.Progress = taskProgress.LevelTask.Goal;
                        taskProgress.IsComplete = true;
                    }
                }
                else if (runescapeLootAlias.Any(l => string.Equals(l.Lootname, loot.Loot, StringComparison.OrdinalIgnoreCase)))
                {
                    RunescapeLootAlias lootAlias = runescapeLootAlias.Where(l => string.Equals(l.Lootname, loot.Loot, StringComparison.OrdinalIgnoreCase)).First();

                    // REminder: add these to a config, hard coded string comparisons. 
                    if ((lootAlias.Mobname == "Skilling" || lootAlias.Mobname == "Any Boss") && (lootAlias.Lootalias == taskProgress.LevelTask.Name))
                    {
                        taskProgress.Progress += loot.Quantity;

                        if (taskProgress.Progress > taskProgress.LevelTask.Goal)
                        {
                            taskProgress.Progress = taskProgress.LevelTask.Goal;
                            taskProgress.IsComplete = true;
                        }
                    }

                    if ((lootAlias.Mobname == loot.Mobname) && (lootAlias.Lootalias == taskProgress.LevelTask.Name))
                    {
                        taskProgress.Progress += loot.Quantity;

                        if (taskProgress.Progress > taskProgress.LevelTask.Goal)
                        {
                            taskProgress.Progress = taskProgress.LevelTask.Goal;
                            taskProgress.IsComplete = true;
                        }
                    }
                }

                await _levelTaskProgress.UpdateAsync(taskProgress);
            }

        }
    }
}
