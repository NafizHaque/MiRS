using MediatR;
using MiRs.Domain.DTOs.RuneHunter;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.Game
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateGameMetadataRequest : IRequest<UpdateGameMetadataResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public IEnumerable<CategoryDto> Categories { get; set; }

        /// <summary>
        /// Validate method used to impliment Validations on request arguments.
        /// </summary>
        public void Validate()
        {
            if (Categories is null || !Categories.Any())
            {
                throw new BadRequestException("Categories is empty");
            }

            IEnumerable<LevelDto> levels = Categories
                  .SelectMany(c => c.Levels ?? Enumerable.Empty<LevelDto>());

            if (!levels.Any())
                throw new BadRequestException("Levels is empty");

            IEnumerable<LevelTaskDto> tasks = levels
                .SelectMany(l => l.LevelTasks ?? Enumerable.Empty<LevelTaskDto>());

            if (!tasks.Any())
                throw new BadRequestException("Level Tasks is empty");
        }
    }
}
