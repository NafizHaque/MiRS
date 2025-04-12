using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Domain.Logging
{
    /// <summary>
    /// The enum for setting the logging event numbers, this is used to help query logs for an event type in App Insights.
    /// </summary>
    public enum LoggingEvents
    {
        /// <summary>
        /// The event id to be used when logging calls related to getting Guild Teams.
        /// </summary>
        GetGuildTeam = 1000,

        /// <summary>
        /// The event id to be used when logging calls related to creating Guild Teams.
        /// </summary>
        CreateGuildTeam = 1001,

        /// <summary>
        /// The event id to be used when logging calls related to registering user.
        /// </summary>
        RegisterUser = 2000,
    }
}
