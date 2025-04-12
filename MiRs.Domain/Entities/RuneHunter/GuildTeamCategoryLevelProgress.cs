using MiRs.Domain.Entities.RuneHunterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Domain.Entities.RuneHunter
{
    public class GuildTeamCategoryLevelProgress
    {
        public int Id { get; set; }

        public bool IsComplete { get; set; }

        public bool IsActive { get; set; }

        public DateTimeOffset LastUpdated { get; set; }

        public int LevelId { get; set; }

        public Level Level { get; set; }

        public int CategoryProgressId { get; set; }

        public GuildTeamCategoryProgress CategoryProgress { get; set; }

        public ICollection<GuildTeamLevelTaskProgress> LevelTaskProgress { get; set; }

    }
}
