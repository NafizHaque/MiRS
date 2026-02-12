using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.DataAccess;
using MiRs.Domain.Configurations;
using MiRs.Domain.DTOs.RuneHunter;
using MiRs.Domain.Entities.RuneHunterData;
using MiRs.Domain.Logging;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.Game;
using MiRS.Gateway.DataAccess;

namespace MiRs.Interactors.RuneHunter.Game
{
    public class UpdateGameMetadataInteractor : RequestHandler<UpdateGameMetadataRequest, UpdateGameMetadataResponse>
    {
        private readonly RuneHunterDbContext _context;
        private readonly IGenericSQLRepository<Category> _category;
        private readonly IGenericSQLRepository<Level> _level;
        private readonly IGenericSQLRepository<LevelTask> _levelTask;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetGameMetadataInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="category">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public UpdateGameMetadataInteractor(
            ILogger<UpdateGameMetadataInteractor> logger,
            IGenericSQLRepository<Category> category,
            IGenericSQLRepository<Level> level,
            IGenericSQLRepository<LevelTask> levelTask,
            RuneHunterDbContext context,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _category = category;
            _level = level;
            _levelTask = levelTask;
            _context = context;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to update game state.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<UpdateGameMetadataResponse> HandleRequest(UpdateGameMetadataRequest request, UpdateGameMetadataResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.GameGetMetadata, "Retrieving current game Categories, Levels and Tasks.");

            IList<Category> currentGameCategories = (await _category.GetAllEntitiesAsync(c => true, default, c => c.Include(c => c.Level).ThenInclude(l => l.LevelTasks))).ToList();

            foreach (CategoryDto catUpsert in request.Categories)
            {
                await UpsertCategory(catUpsert, currentGameCategories);
            }

            await _context.SaveChangesAsync();

            return result;
        }

        private async Task UpsertCategory(CategoryDto catUpsert, IList<Category> currentGameCategories)
        {
            IEnumerable<Level> currentGameLevels = currentGameCategories.SelectMany(c => c.Level);
            Category categoryInProcess;

            if (catUpsert.Id == 0)
            {
                categoryInProcess = await _category.AddAsync(new Category
                {
                    Name = catUpsert.Name,
                });
            }
            else
            {

                Category? categoryToBeUpdated = currentGameCategories.Where(c => c.Id == catUpsert.Id).FirstOrDefault();

                if (categoryToBeUpdated == null)
                    throw new InvalidOperationException($"Category {catUpsert.Id} not found");

                categoryToBeUpdated.Name = catUpsert.Name;

                categoryInProcess = categoryToBeUpdated;
            }

            foreach (LevelDto level in catUpsert.Levels)
            {
                await UpsertLevel(level, categoryInProcess, currentGameLevels);
            }
        }

        private async Task UpsertLevel(LevelDto levelUpsert, Category categoryParent, IEnumerable<Level> currentGameLevels)
        {
            Level levelInProcess;
            IEnumerable<LevelTask> currentGameTasks = currentGameLevels.SelectMany(l => l.LevelTasks);

            if (levelUpsert.Id == 0)
            {
                levelInProcess = await _level.AddAsync(new Level
                {
                    Levelnumber = levelUpsert.Levelnumber,
                    Unlock = levelUpsert.Unlock,
                    UnlockDescription = levelUpsert.UnlockDescription,
                    CategoryId = categoryParent.Id,
                });
            }
            else
            {

                Level? levelToBeUpdated = currentGameLevels.Where(l => l.Id == levelUpsert.Id).FirstOrDefault();

                if (levelToBeUpdated == null)
                    throw new InvalidOperationException($"Level {levelUpsert.Id} not found");

                levelToBeUpdated.Levelnumber = levelUpsert.Levelnumber;
                levelToBeUpdated.Unlock = levelUpsert.Unlock;
                levelToBeUpdated.UnlockDescription = levelUpsert.UnlockDescription;

                levelInProcess = levelToBeUpdated;
            }

            foreach (LevelTaskDto levelTask in levelUpsert.LevelTasks)
            {
                await UpsertTaskLevel(levelTask, levelInProcess, currentGameTasks);
            }
        }

        private async Task UpsertTaskLevel(LevelTaskDto levelTaskUpsert, Level parentLevel, IEnumerable<LevelTask> currentGameLevelTasks)
        {

            if (levelTaskUpsert.Id == 0)
            {
                await _levelTask.AddAsync(new LevelTask
                {
                    Name = levelTaskUpsert.Name,
                    Goal = levelTaskUpsert.Goal,
                    LevelId = parentLevel.Id,
                    Levelnumber = parentLevel.Levelnumber
                });
            }
            else
            {

                LevelTask? taskToBeUpdated = currentGameLevelTasks.Where(lt => lt.Id == levelTaskUpsert.Id).FirstOrDefault();

                if (taskToBeUpdated == null)
                    throw new InvalidOperationException($"Task {levelTaskUpsert.Id} not found");

                taskToBeUpdated.Name = levelTaskUpsert.Name;
                taskToBeUpdated.Goal = levelTaskUpsert.Goal;
            }
        }
    }
}
