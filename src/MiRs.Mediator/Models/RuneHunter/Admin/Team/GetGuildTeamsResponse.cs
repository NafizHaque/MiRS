using MiRs.Domain.Entities.RuneHunter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Mediator.Models.RuneHunter.Admin.Team
{
    public class GetGuildTeamsResponse
    {
        public IEnumerable<GuildTeam> GuildTeams { get; set; } = Enumerable.Empty<GuildTeam>();
    }
}
