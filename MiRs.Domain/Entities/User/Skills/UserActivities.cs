using MiRs.Domain.Entities.User.Skills.Skill_Object;
using System.Text.Json.Serialization;

namespace MiRs.Domain.Entities.User.Skills
{
    public class UserActivities
    {
        /// <summary>
        /// Gets or sets the Overall.
        /// </summary>
        [JsonPropertyName("league_points")]
        public Activity LeaguePoints { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the Overall.
        /// </summary>
        public Activity BountyHunterHunter { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the Overall.
        /// </summary>
        public Activity BountyHunterRogue { get; set; } = new Activity();


        /// <summary>
        /// Gets or sets the ClueScrollAll.
        /// </summary>
        [JsonPropertyName("league_points")]
        public Activity ClueScrollAll { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the ClueScrollBeginner.
        /// </summary>
        public Activity ClueScrollBeginner { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the ClueScrollEasy.
        /// </summary>
        public Activity ClueScrollEasy { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the ClueScrollMedium.
        /// </summary>
        public Activity ClueScrollMedium { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the ClueScrollHard.
        /// </summary>
        public Activity ClueScrollHard { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the ClueScrollElite.
        /// </summary>
        public Activity ClueScrollElite { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the ClueScrollMaster.
        /// </summary>
        public Activity ClueScrollMaster { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the LastManStanding.
        /// </summary>
        public Activity LastManStanding { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the PVPArena.
        /// </summary>
        public Activity PVPArena { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the SoulWarsZeal.
        /// </summary>
        public Activity SoulWarsZeal { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the GuardiansOfTheRift.
        /// </summary>
        public Activity GuardiansOfTheRift { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the ColosseumGlory.
        /// </summary>
        public Activity ColosseumGlory { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the CollectionLogged.
        /// </summary>
        public Activity CollectionLogged { get; set; } = new Activity();


    }
}
