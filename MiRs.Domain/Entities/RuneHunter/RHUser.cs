using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Domain.Entities.RuneHunter
{
    public class RHUser
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PreviousUsername { get; set; } = string.Empty;

        public ICollection<RHUserToTeam> UserToTeams { get; set; }
    }
    
}
