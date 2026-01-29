using MiRs.Domain.Entities.RuneHunter;

namespace MiRs.API.ApiDTOs
{
    /// <summary>
    /// The New Team DTO for Event.
    /// </summary>
    public class AddNewTeamToEvent
    {
        /// <summary>
        /// Gets or sets the EventId.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets the AddExistingTeamToggle.
        /// </summary>
        public bool AddExistingTeamToggle { get; set; } = false;

        /// <summary>
        /// Gets or sets the NewTeamToBeCreated.
        /// </summary>
        public GuildTeam NewTeamToBeCreated { get; set; } = new GuildTeam();
    }
}
