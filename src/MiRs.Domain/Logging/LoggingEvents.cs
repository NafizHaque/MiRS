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
        UpdateGuildTeam = 1003,

        /// <summary>
        /// The event id to be used when logging calls related to creating Guild Teams.
        /// </summary>
        GetGuildEvent = 1101,

        /// <summary>
        /// The event id to be used when logging calls related to creating Guild Teams.
        /// </summary>
        CreateGuildEvent = 1102,

        /// <summary>
        /// The event id to be used when logging calls related to getting Teams from Guild Event.
        /// </summary>
        GetTeamsFromEvent = 1103,

        /// <summary>
        /// The event id to be used when logging calls related to updating Guild Teams.
        /// </summary>
        UpdateEventVerify = 1104,

        /// <summary>
        /// The event id to be used when logging calls related to creating event team progress.
        /// </summary>
        InitialiseEventTeamProgress = 1105,

        /// <summary>
        /// The event id to be used when logging calls related to registering user.
        /// </summary>
        RegisterUser = 2000,

        /// <summary>
        /// The event id to be used when logging calls related to registering user.
        /// </summary>
        UserToTeamJoin = 2001,

        /// <summary>
        /// The event id to be used when logging calls related to registering user.
        /// </summary>
        UserSearch = 2002,

        /// <summary>
        /// The event id to be used when logging calls related to get current user events.
        /// </summary>
        CurrentUserEvents = 2003,

        /// <summary>
        /// The event id to be used when logging calls related to logging user loot.
        /// </summary>
        GameLogLoot = 3000,

        /// <summary>
        /// The event id to be used when logging calls related to processing user loot.
        /// </summary>
        GameProcessLoot = 3001,

        /// <summary>
        /// The event id to be used when logging calls related to game state updating.
        /// </summary>
        GameStateUpdate = 3002,

        /// <summary>
        /// The event id to be used when logging calls related to game getting meta data.
        /// </summary>
        GameGetMetadata = 3003,

        /// <summary>
        /// The event id to be used when logging calls related to game updating meta data.
        /// </summary>
        GameUpdateMetadata = 3004,

        /// <summary>
        /// The event id to be used when logging calls related to game updating event winners.
        /// </summary>
        GameUpdateEventWinners = 3005,
    }
}
