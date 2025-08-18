using MiRs.Domain.Entities.User.Skills.Skill_Object;
using System.Text.Json.Serialization;

namespace MiRs.Domain.Entities.User.Skills
{
    /// <summary>
    /// User Activities holds data about the Users Minigame Activities.
    /// </summary>
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
        [JsonPropertyName("bounty_hunter_hunter")]
        public Activity BountyHunterHunter { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the Overall.
        /// </summary>
        [JsonPropertyName("bounty_hunter_rogue")]
        public Activity BountyHunterRogue { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the ClueScrollAll.
        /// </summary>
        [JsonPropertyName("clue_scrolls_all")]
        public Activity ClueScrollsAll { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the ClueScrollBeginner.
        /// </summary>
        [JsonPropertyName("clue_scrolls_beginner")]
        public Activity ClueScrollBeginner { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the ClueScrollEasy.
        /// </summary>
        [JsonPropertyName("clue_scrolls_easy")]
        public Activity ClueScrollsEasy { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the ClueScrollMedium.
        /// </summary>
        [JsonPropertyName("clue_scrolls_medium")]
        public Activity ClueScrollsMedium { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the ClueScrollHard.
        /// </summary>
        [JsonPropertyName("clue_scrolls_hard")]
        public Activity ClueScrollsHard { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the ClueScrollElite.
        /// </summary>
        [JsonPropertyName("clue_scrolls_Elite")]
        public Activity ClueScrollsElite { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the ClueScrollMaster.
        /// </summary>
        [JsonPropertyName("clue_scrolls_Master")]
        public Activity ClueScrollsMaster { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the LastManStanding.
        /// </summary>
        [JsonPropertyName("last_man_standing")]
        public Activity LastManStanding { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the PVPArena.
        /// </summary>
        [JsonPropertyName("pvp_arena")]
        public Activity PVPArena { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the SoulWarsZeal.
        /// </summary>
        [JsonPropertyName("soul_wars_zeal")]
        public Activity SoulWarsZeal { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the GuardiansOfTheRift.
        /// </summary>
        [JsonPropertyName("guardians_of_the_rift")]
        public Activity GuardiansOfTheRift { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the ColosseumGlory.
        /// </summary>
        [JsonPropertyName("colosseum_glory")]
        public Activity ColosseumGlory { get; set; } = new Activity();

        /// <summary>
        /// Gets or sets the CollectionLogged.
        /// </summary>
        [JsonPropertyName("collections_logged")]
        public Activity CollectionsLogged { get; set; } = new Activity();

    }
}
