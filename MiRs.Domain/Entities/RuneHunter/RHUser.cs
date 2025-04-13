using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Domain.Entities.RuneHunter
{
    public class RHUser
    {
        public ulong UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PreviousUsername { get; set; } = string.Empty;

        public DateTimeOffset CreatedDate { get; set; } 

        public ICollection<RHUserToTeam>? UserToTeams { get; set; }
    }
    
}
