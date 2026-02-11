using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.DTOs.RuneHunter;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Entities.RuneHunterData;
using MiRs.Domain.Entities.RuneHunterData.Enums;
using MiRs.Domain.Logging;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.Game;
using MiRs.Mediator.Models.RuneHunter.User;
using MiRs.Utils.Helpers;
using MiRS.Gateway.DataAccess;

namespace MiRs.Interactors.RuneHunter.Game
{
    public class ProcessUserLootInteractor : RequestHandler<ProcessUserLootRequest, ProcessUserLootResponse>
    {
        private readonly IGenericSQLRepository<RHUserRawLoot> _rhUserRawLoot;
        private readonly IGenericSQLRepository<RunescapeLootAlias> _rhLootAlias;

        private readonly IGenericSQLRepository<GuildTeamLevelTaskProgress> _levelTaskProgress;
        private readonly IGenericSQLRepository<GuildTeamCategoryProgress> _categoryProgress;

        private readonly AppSettings _appSettings;
        private readonly ISender _mediator;

        private IEnumerable<GuildTeamCategoryProgress> _categoryProgressData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessUserLootInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public ProcessUserLootInteractor(
            ILogger<ProcessUserLootInteractor> logger,
            IGenericSQLRepository<RHUserRawLoot> rhUserRawLoot,
            IGenericSQLRepository<GuildTeamLevelTaskProgress> levelTaskProgress,
            IGenericSQLRepository<GuildTeamCategoryProgress> categoryProgress,
            IGenericSQLRepository<RunescapeLootAlias> rhLootAlias,
            ISender mediator,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _rhUserRawLoot = rhUserRawLoot;
            _levelTaskProgress = levelTaskProgress;
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
            IList<GuildTeamLevelTaskProgress> levelTasksProgress = (await _levelTaskProgress.GetAllEntitiesAsync(t => t.IsComplete == false && t.GuildEventTeamId == userEvent.EventTeam.Id, default, lt => lt.Include(ltt => ltt.LevelTask))).ToList();

            _categoryProgressData = (await _categoryProgress.GetAllEntitiesAsync(
              t => t.GuildEventTeamId == userEvent.EventTeam.Id,
              default,
              cp => cp.Include(c => c.Category)
                      .Include(lp => lp.CategoryLevelProcess)
                          .ThenInclude(l => l.Level)))
              .ToList();

            if (!levelTasksProgress.Any())
            {
                Logger.LogError((int)LoggingEvents.GameProcessLoot, "No levelTasksProgress for event: {userEvent.EventTeam.Id} could be found!", userEvent.EventTeam.Id);

                return;
            }

            int minLevel = levelTasksProgress.Min(t => t.LevelTask.Levelnumber);

            List<GuildTeamLevelTaskProgress> lowestIncompleteTasks = levelTasksProgress
                .Where(t => t.LevelTask.Levelnumber == minLevel)
                .ToList();

            bool lootUnlockCheck = await LootUnlockCheck(loot, runescapeLootAlias);

            RHUserRawLoot userMultipliedLoot = await CalculateLootMultiplier(loot, runescapeLootAlias);

            foreach (GuildTeamLevelTaskProgress taskProgress in lowestIncompleteTasks)
            {

                if (string.Equals(taskProgress.LevelTask.Name, loot.Loot, StringComparison.OrdinalIgnoreCase) && lootUnlockCheck)
                {

                    taskProgress.Progress += userMultipliedLoot.Quantity;
                    taskProgress.LastUpdated = DateTimeOffset.UtcNow;

                    if (taskProgress.Progress > taskProgress.LevelTask.Goal)
                    {
                        taskProgress.Progress = taskProgress.LevelTask.Goal;
                        taskProgress.IsComplete = true;
                    }
                }
                else if (runescapeLootAlias.Any(l => string.Equals(l.Lootname, loot.Loot, StringComparison.OrdinalIgnoreCase)) && lootUnlockCheck)
                {
                    RunescapeLootAlias lootAlias = runescapeLootAlias.Where(l => string.Equals(l.Lootname, loot.Loot, StringComparison.OrdinalIgnoreCase)).First();

                    // REminder: add these to a config, hard coded string comparisons. 
                    if (string.Equals(taskProgress.LevelTask.Name, lootAlias.Lootalias, StringComparison.OrdinalIgnoreCase))
                    {
                        taskProgress.Progress += userMultipliedLoot.Quantity;
                        taskProgress.LastUpdated = DateTimeOffset.UtcNow;

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

        private async Task<bool> LootUnlockCheck(RHUserRawLoot loot, IEnumerable<RunescapeLootAlias> runescapeLootAlias)
        {
            RunescapeLootAlias lootAlias = runescapeLootAlias.Where(l => string.Equals(l.Lootname, loot.Loot, StringComparison.OrdinalIgnoreCase)).First();

            string[] specialEncounters = { "Lunar Chest",
                                        "Fortis Colosseum",
                                        "Tombs of Amascut",
                                        "Chambers of Xeric",
                                        "Theatre of Blood" };

            string? matchedEncounter = specialEncounters
                .FirstOrDefault(e => string.Equals(e, lootAlias.Mobname, StringComparison.OrdinalIgnoreCase));

            if (matchedEncounter is not null)
            {
                IEnumerable<GuildTeamCategoryLevelProgress> specialEncountersActive = _categoryProgressData
                        .Where(c => string.Equals(c.Category.name, "Armoury", StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault().CategoryLevelProcess.Where(lp => lp.IsActive);

                if (!specialEncountersActive.Any())
                {
                    return false;
                }

                if (specialEncountersActive.Any(l => string.Equals(l.Level.Unlock, matchedEncounter, StringComparison.OrdinalIgnoreCase)))
                {
                    return true;
                }
            }

            if (loot.MobLevel <= 200)
            {
                return true;
            }

            IEnumerable<GuildTeamCategoryLevelProgress> MonsterLevelsActive = _categoryProgressData
            .Where(c => string.Equals(c.Category.name, "Training Area", StringComparison.OrdinalIgnoreCase))
            .FirstOrDefault().CategoryLevelProcess.Where(lp => lp.IsActive);

            if (MonsterLevelsActive.Any())
            {
                GuildTeamCategoryLevelProgress highestLevelActive = MonsterLevelsActive
                 .OrderByDescending(lp => lp.Level.Levelnumber)
                 .FirstOrDefault();

                if (loot.MobLevel <= int.Parse(highestLevelActive.Level.Unlock))
                {
                    return true;
                }
            }
            return false;

        }

        private async Task<RHUserRawLoot> CalculateLootMultiplier(RHUserRawLoot loot, IEnumerable<RunescapeLootAlias> runescapeLootAlias)
        {
            RunescapeLootAlias lootAlias = runescapeLootAlias.Where(l => string.Equals(l.Lootname, loot.Loot, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (lootAlias is null)
            {
                return loot;
            }

            if (lootAlias.Mobname != "Skilling")
            {
                return loot;
            }

            LootAliasSkillingCategories category = EnumHelper.ParseOrDefault<LootAliasSkillingCategories>(lootAlias.Lootalias);

            RHUserRawLoot updatedUserLoot = new RHUserRawLoot
            {
                Quantity = (int)Math.Ceiling((double)loot.Quantity * (await GetCategoryMultiplier(category))),
            };

            return updatedUserLoot;
        }

        private async Task<double> GetCategoryMultiplier(LootAliasSkillingCategories category)
        {
            if (!_categoryProgressData.Any())
                return 1.0;

            Dictionary<LootAliasSkillingCategories, string> categoryMap = new Dictionary<LootAliasSkillingCategories, string>
            {
                [LootAliasSkillingCategories.Gems] = "Crafting Guild",
                [LootAliasSkillingCategories.Herbs] = "Herbalist Guild",
                [LootAliasSkillingCategories.Seeds] = "Farming Guild",
                [LootAliasSkillingCategories.Ores] = "Mining Guild",
                [LootAliasSkillingCategories.Runes] = "Runecraft Guild",
                [LootAliasSkillingCategories.Logs] = "Woodcutting Guild"
            };

            if (!categoryMap.TryGetValue(category, out string guildName))
                return 1.0;

            return RetrieveMultiplierValue(_categoryProgressData, guildName);
        }

        private double RetrieveMultiplierValue(IEnumerable<GuildTeamCategoryProgress> categoriesProgress, string catName)
        {
            IEnumerable<GuildTeamCategoryLevelProgress> levelsProgressActive = categoriesProgress
                                    .Where(c => string.Equals(c.Category.name, catName, StringComparison.OrdinalIgnoreCase))
                                    .FirstOrDefault().CategoryLevelProcess.Where(lp => lp.IsActive); ;

            if (!levelsProgressActive.Any())
            {
                return 1.0;
            }

            GuildTeamCategoryLevelProgress highestLevelActive = levelsProgressActive
                 .OrderByDescending(lp => lp.Level.Levelnumber)
                 .FirstOrDefault();

            if (highestLevelActive == null)
            {
                return 1.0;
            }

            if (!double.TryParse(highestLevelActive.Level.Unlock, out double multiplier))
            {
                Logger.LogError((int)LoggingEvents.GameProcessLoot, "Multiplier could not be concerted to double: levelId: {levelid}", highestLevelActive.LevelId);
            }

            return multiplier;
        }

    }
}
