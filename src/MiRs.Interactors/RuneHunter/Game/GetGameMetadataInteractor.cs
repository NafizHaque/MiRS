using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.DTOs.RuneHunter;
using MiRs.Domain.Entities.RuneHunterData;
using MiRs.Domain.Logging;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.Game;
using MiRS.Gateway.DataAccess;

namespace MiRs.Interactors.RuneHunter.Game
{
    public class GetGameMetadataInteractor : RequestHandler<GetGameMetadataRequest, GetGameMetadataResponse>
    {
        private readonly IGenericSQLRepository<Category> _category;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetGameMetadataInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="category">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public GetGameMetadataInteractor(
            ILogger<ProcessUserLootInteractor> logger,
            IGenericSQLRepository<Category> category,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _category = category;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to Get game metadata.
        /// </summary>
        /// <param name="request">The request to get game metadata.</param>
        /// <param name="result">Category object that was queried.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the category object that contains its level and leveltask children.</returns>
        protected override async Task<GetGameMetadataResponse> HandleRequest(GetGameMetadataRequest request, GetGameMetadataResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.GameGetMetadata, "Retrieving current game Categories, Levels and Tasks.");

            IList<Category> categories = (await _category.GetAllEntitiesAsync(c => true, default, c => c.Include(l => l.Level).ThenInclude(t => t.LevelTasks))).ToList();

            result.Categories = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Levels = c.Level.Select(l => new LevelDto
                {
                    Id = l.Id,
                    Levelnumber = l.Levelnumber,
                    Unlock = l.Unlock,
                    UnlockDescription = l.UnlockDescription,
                    CategoryId = l.CategoryId,
                    LevelTasks = l.LevelTasks.Select(t => new LevelTaskDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Goal = t.Goal,
                        LevelId = t.LevelId,
                        Levelnumber = t.Levelnumber,

                    }).ToList()
                }).ToList()
            }).ToList();

            return result;
        }
    }
}
