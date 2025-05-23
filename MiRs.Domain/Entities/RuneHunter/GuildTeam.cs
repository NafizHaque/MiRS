﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Domain.Entities.RuneHunter
{
    public class GuildTeam
    {
        public int Id { get; set; }

        public ulong GuildId { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public DateTimeOffset CreatedDate { get; set; }

        public ICollection<RHUserToTeam>? UsersInTeam { get; set; }

        public ICollection<GuildEventTeam>? EventTeams { get; set; }
    }
}
