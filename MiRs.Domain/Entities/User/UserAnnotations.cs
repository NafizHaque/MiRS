using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Domain.Entities.User
{
    public class UserAnnotations
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int PlayerID { get; set; }

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the CreatedDate.
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }
    }
}
