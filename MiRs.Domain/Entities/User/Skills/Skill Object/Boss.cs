using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Domain.Entities.User.Skills.Skill_Object
{
    public class Boss
    {
        /// <summary>
        /// Gets or sets the Metric.
        /// </summary>
        public string Metric { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Kills.
        /// </summary>
        public int Kills { get; set; }

        /// <summary>
        /// Gets or sets the Rank.
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets the Ehp.
        /// </summary>
        public double Ehb { get; set; }
    }
}
