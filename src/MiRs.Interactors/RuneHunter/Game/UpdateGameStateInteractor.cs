using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Logging;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.Game;
using MiRS.Gateway.DataAccess;

namespace MiRs.Interactors.RuneHunter.Game
{
    public class UpdateGameStateInteractor : RequestHandler<UpdateGameStateRequest, UpdateGameStateResponse>
    {
        private readonly IGenericSQLRepository<GuildTeamCategoryProgress> _categoryProgress;
        private readonly IGenericSQLRepository<GuildTeamCategoryLevelProgress> _levelProgress;

        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateGameStateInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public UpdateGameStateInteractor(
            ILogger<ProcessUserLootInteractor> logger,
            IGenericSQLRepository<GuildTeamCategoryLevelProgress> levelProgress,
            IGenericSQLRepository<GuildTeamCategoryProgress> categoryProgress,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _levelProgress = levelProgress;
            _categoryProgress = categoryProgress;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to update game state.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<UpdateGameStateResponse> HandleRequest(UpdateGameStateRequest request, UpdateGameStateResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.GameStateUpdate, "Updating Event Game Stated.");

            IList<GuildTeamCategoryLevelProgress> levelProgress = (await _levelProgress.GetAllEntitiesAsync(t => t.IsComplete == false, default, lt => lt.Include(ltt => ltt.LevelTaskProgress))).ToList();

            foreach (GuildTeamCategoryLevelProgress level in levelProgress)
            {
                if (level.LevelTaskProgress.Any(t => !t.IsComplete))
                {
                    continue;
                }
                else
                {
                    level.IsComplete = true;
                    level.IsActive = true;
                    level.LastUpdated = DateTimeOffset.UtcNow;
                    await _levelProgress.UpdateAsync(level);
                }
            }

            IList<GuildTeamCategoryProgress> categoryProgress = (await _categoryProgress.GetAllEntitiesAsync(t => t.IsComplete == false, default, lt => lt.Include(ltt => ltt.CategoryLevelProcess))).ToList();

            foreach (GuildTeamCategoryProgress cat in categoryProgress)
            {
                if (cat.CategoryLevelProcess.Any(t => !t.IsComplete))
                {
                    continue;
                }
                else
                {
                    cat.IsComplete = true;
                    await _categoryProgress.UpdateAsync(cat);
                }
            }

            return result;
        }
    }
}
