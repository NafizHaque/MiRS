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

        public int UserId { get; set; }

        public int TeamId { get; set; }

        public RHUser User { get; set; } = new RHUser();

        public GuildTeams Team { get; set; } = new GuildTeams();
    }
}
