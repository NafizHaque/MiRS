using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Domain.Entities.RuneHunter
{
    public class RHUserRawLoot
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public ulong UserId { get; set; }

        public string Loot { get; set; }

        public int Quantity { get; set; }

        public string Mobname { get; set; }

        public DateTimeOffset DateLogged { get; set; }

        public bool Processed { get; set; }
    }
}
