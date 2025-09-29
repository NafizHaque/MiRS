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
        AdminEvent = 1000,

        /// <summary>
        /// The event id to be used when logging calls related to getting Guild Teams.
        /// </summary>
        GetGuildTeam = 1001,

        /// <summary>
        /// The event id to be used when logging calls related to creating Guild Teams.
        /// </summary>
        CreateGuildTeam = 1002,

        /// <summary>
        /// The event id to be used when logging calls related to creating Guild Teams.
        /// </summary>
        GetGuildEvent = 1101,

        /// <summary>
        /// The event id to be used when logging calls related to creating Guild Teams.
        /// </summary>
        CreateGuildEvent = 1102,

        /// <summary>
        /// The event id to be used when logging calls related to registering user.
        /// </summary>
        RegisterUser = 2000,

        /// <summary>
        /// The event id to be used when logging calls related to registering user.
        /// </summary>
        UserToTeamJoin = 2001,

        /// <summary>
        /// The event id to be used when logging calls related to logging user loot.
        /// </summary>
        GameLogLoot = 3000,

        /// <summary>
        /// The event id to be used when logging calls related to processing user loot.
        /// </summary>
        GameProcessLoot = 3001,
    }
}
