using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Domain.Entities.RuneHunter
{
    public class GuildEvent
    {
        public int Id { get; set; }

        public ulong GuildId { get; set; }

        public string Eventname { get; set; } = string.Empty;

        public bool EventActive { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset EventStart { get; set; }

        public DateTimeOffset EventEnd { get; set; }

        public ICollection<GuildEventTeam>? EventTeams { get; set; }
    }
}
