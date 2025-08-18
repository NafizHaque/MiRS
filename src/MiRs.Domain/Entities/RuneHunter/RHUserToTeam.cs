using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Domain.Entities.RuneHunter
{
    public class RHUserToTeam
    {

        public int Id { get; set; }

        public ulong UserId { get; set; }

        public int TeamId { get; set; }

        public RHUser? User { get; set; }

        public GuildTeam? Team { get; set; }
    }
}
