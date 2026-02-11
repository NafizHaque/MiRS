using MiRs.Domain.DTOs.RuneHunter;
using MiRs.Domain.Entities.RuneHunter;

namespace MiRs.Domain.Mappers
{
    public class GameMapper
    {
        /// <summary>
        /// Maps data to the User object.
        /// </summary>
        /// <param name="userData">userFileData object.</param>
        /// <returns>UserTableEntity object.</returns>
        public CategoryProgress Map(GuildTeamCategoryProgress tcp)
        {

            if (tcp.Category == null)
                throw new Exception("Category is null");

            if (tcp.CategoryLevelProcess == null)
                throw new Exception("CategoryLevelProcess is null");

            CategoryProgress dto = new CategoryProgress
            {
                Id = tcp.Id,
                IsComplete = tcp.IsComplete,
                CategoryId = tcp.CategoryId,
            };

            dto.Category = new CategoryDto
            {
                Id = tcp.Category.Id,
                Name = tcp.Category.name,
            };

            dto.CategoryLevelProcess = tcp.CategoryLevelProcess.Select(clp =>
            {
                if (clp.Level == null)
                    throw new Exception($"Level is null for CLP {clp.Id}");

                if (clp.LevelTaskProgress == null)
                    throw new Exception($"LevelTaskProgress is null for CLP {clp.Id}");

                return new CategoryLevelProgress
                {
                    Id = clp.Id,
                    IsComplete = clp.IsComplete,
                    IsActive = clp.IsActive,
                    LastUpdated = clp.LastUpdated,
                    LevelId = clp.LevelId,
                    CategoryProgressId = clp.CategoryProgressId,
                    Level = new LevelDto
                    {
                        Id = clp.Level.Id,
                        Levelnumber = clp.Level.Levelnumber,
                        Unlock = clp.Level.Unlock,
                        UnlockDescription = clp.Level.UnlockDescription,
                        CategoryId = clp.Level.CategoryId,
                    },
                    LevelTaskProgress = clp.LevelTaskProgress.Select(ltp => new LevelTaskProgress
                    {
                        Id = ltp.Id,
                        Progress = ltp.Progress,
                        IsComplete = ltp.IsComplete,
                        LastUpdated = ltp.LastUpdated,
                        GuildEventTeamId = ltp.GuildEventTeamId,
                        CategoryLevelProcessId = ltp.CategoryLevelProcessId,
                        LevelTaskId = ltp.LevelTaskId,
                        LevelTask = new LevelTaskDto()
                        {
                            Id = ltp.LevelTask.Id,
                            Name = ltp.LevelTask.Name,
                            Goal = ltp.LevelTask.Goal,
                            LevelId = ltp.LevelTask.LevelId,
                            Levelnumber = clp.Level.Levelnumber,

                        }
                    }
                )
                };
            });

            return dto;
        }
    }
}
