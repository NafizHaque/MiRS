using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Domain.Entities.RuneHunterData
{
    public class Levels
    {
        public int Id {  get; set; }

        public int Level { get; set; } 

        public int Goal { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int TaskId { get; set; }

        public LevelTask Task { get; set; }

    }
}
