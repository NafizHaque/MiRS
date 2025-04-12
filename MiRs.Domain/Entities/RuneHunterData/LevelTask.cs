using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Domain.Entities.RuneHunterData
{
    public class LevelTask
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Level { get; set; }

        public int Goal { get; set; }

        public int Unlock { get; set; }

        public string UnlockDescription { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
