using MiRs.Mediator.Models.RuneHunter.Game;
using MiRs.Mediator;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;
using MiRS.Gateway.DataAccess;
using MiRs.Domain.Entities.RuneHunterData;

namespace MiRs.Interactors.RuneHunter.Game
{
    public class InitaliseTeamProgressInteractor : RequestHandler<InitaliseTeamProgressRequest, InitaliseTeamProgressResponse>
    {
        private readonly IGenericSQLRepository<LevelTask> _levelTask;
        private readonly IGenericSQLRepository<GuildTeamLevelTaskProgress> _levelTaskProgress;

        private readonly IGenericSQLRepository<Level> _level;
        private readonly IGenericSQLRepository<GuildTeamCategoryLevelProgress> _levelProgress;

        private readonly IGenericSQLRepository<Category> _category;
        private readonly IGenericSQLRepository<GuildTeamCategoryProgress> _categoryProgress;

        private readonly IGenericSQLRepository<GuildEventTeam> _guildEventTeam;

        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGuildTeamInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public InitaliseTeamProgressInteractor(
            ILogger<LogUserLootInteractor> logger,
            IGenericSQLRepository<LevelTask> levelTask,
            IGenericSQLRepository<GuildTeamLevelTaskProgress> levelTaskProgress,
            IGenericSQLRepository<Level> level,
            IGenericSQLRepository<GuildTeamCategoryLevelProgress> levelProgress,
            IGenericSQLRepository<Category> category,
            IGenericSQLRepository<GuildTeamCategoryProgress> categoryProgress,
            IGenericSQLRepository<GuildEventTeam> guildEventTeam,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _level = level;
            _levelTask = levelTask;
            _levelTaskProgress = levelTaskProgress;
            _levelProgress = levelProgress;
            _category = category;
            _categoryProgress = categoryProgress;
            _guildEventTeam = guildEventTeam;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to Initalise Team Progress.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<InitaliseTeamProgressResponse> HandleRequest(InitaliseTeamProgressRequest request, InitaliseTeamProgressResponse result, CancellationToken cancellationToken)
        {
            int guildEventTeamId = (await _guildEventTeam.Query(et => et.EventId == request.EventId && et.TeamId == request.TeamId)).Select(i => i.Id).FirstOrDefault();

            if (guildEventTeamId <= 0)
            {

                throw new BadRequestException("Team is not registered to this event!");
            }

            if ((await _categoryProgress.Query(c => c.GuildEventTeamId == guildEventTeamId)).Any())
            {
                throw new BadRequestException("Progress already initalised!");
            }

            IEnumerable<Category> categories = await _category.GetAllEntitiesAsync();

            foreach (Category category in categories.ToList())
            {
                await _categoryProgress.AddAsync(
                    new GuildTeamCategoryProgress
                    {
                        IsComplete = false,
                        CategoryId = category.Id,
                        GuildEventTeamId = guildEventTeamId,
                    });
            }

            IEnumerable<GuildTeamCategoryProgress> categoriesProgress = await _categoryProgress.GetAllEntitiesAsync();

            IEnumerable<Level> levels = await _level.GetAllEntitiesAsync();

            foreach (GuildTeamCategoryProgress categoryProgress in categoriesProgress.ToList())
            {
                IList<Level> levelsForCategory = levels.Where(l => l.CategoryId == categoryProgress.CategoryId).ToList();

                foreach (Level level in levelsForCategory)
                {
                    await _levelProgress.AddAsync(
                        new GuildTeamCategoryLevelProgress
                        {
                            IsComplete = false,
                            IsActive = false,
                            LastUpdated = DateTimeOffset.UtcNow,
                            LevelId = level.Id,
                            CategoryProgressId = categoryProgress.Id,

                        });
                }
            }

            IEnumerable<GuildTeamCategoryLevelProgress> levelsProgress = await _levelProgress.GetAllEntitiesAsync();

            IEnumerable<LevelTask> levelTasks = await _levelTask.GetAllEntitiesAsync();

            foreach (GuildTeamCategoryLevelProgress levelProgress in levelsProgress.ToList())
            {
                IList<LevelTask> levelTasksForLevel = levelTasks.Where(l => l.LevelId == levelProgress.LevelId).ToList();

                foreach (LevelTask levelTask in levelTasksForLevel)
                {
                    await _levelTaskProgress.AddAsync(
                        new GuildTeamLevelTaskProgress
                        {
                            Progress = 0,
                            IsComplete = false,
                            LastUpdated = DateTimeOffset.UtcNow,
                            CategoryLevelProcessId = levelProgress.Id,
                            LevelTaskId = levelTask.Id,
                            GuildEventTeamId = guildEventTeamId,

                        });
                }
            }

            return result;
        }
    }
}
