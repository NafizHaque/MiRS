using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Domain.Entities.RuneHunterData
{
    public class Level
    {
        public int Id { get; set; }

        public int Levelnumber { get; set; }

        public int Unlock { get; set; }

        public string UnlockDescription { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<LevelTask>? LevelTasks { get; set; }
    }
}
