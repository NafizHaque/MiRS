using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Domain.Entities.User
{
    class UserLatestSnapshot
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int PlayerID { get; set; }

        /// <summary>
        /// Gets or sets the CreatedAt.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the CreatedAt.
        /// </summary>
        public DateTimeOffset ImportedAt { get; set; }


        /// <summary>
        /// Gets or sets the CreatedAt.
        /// </summary>
        public UserMetrics Data { get; set; } = new UserMetrics();
    }
}
