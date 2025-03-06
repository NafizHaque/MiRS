using MiRs.Domain.Entities.User.Skills.Skill_Object;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Domain.Entities.User.Skills
{
    class UserBosses
    {
        /// <summary>
        /// Gets or sets the Bos Dictionary.
        /// </summary>
        [JsonProperty("bosses")]
        public Dictionary<string, Boss> Bosses { get; set; }
    }
}
