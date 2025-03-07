using MiRS.Gateway.RunescapeClient;
using Flurl.Http;
using MiRs.Domain.Entities.User;
using MiRs.Domain.Entities.User.Skills.Skill_Object;
using System.Text.Json;
using MiRs.Utils.Helpers.Instances;
using MiRs.Utils.Helpers.Interfaces;

namespace MiRs.RunescapeClient
{
    /// <summary>
    /// The client for connection to the WOM API.
    /// </summary>
    public class WOMClient : IRuneClient
    {
        private readonly IJsonSeraliserDefaultOptions _jsonUtils;

        public WOMClient(IJsonSeraliserDefaultOptions jsonUtils)
        {
            _jsonUtils = jsonUtils;
        }

        /// <summary>
        /// The call to get user in RS via the WOM API.
        /// </summary>
        /// <param name="username">The Runescape RSN</param>
        /// <returns>The response status.</returns>
        public async Task<User> GetRuneUser(string username)
        {
            var jsonResponse = await "https://api.wiseoldman.net/v2/"
                .WithHeader("Content-Type", "application/json")
                .AppendPathSegment($"players/{username}")
                .GetStringAsync();

            var user = _jsonUtils.Deserialize<User>(jsonResponse);


            var jsonObject = JsonDocument.Parse(jsonResponse).RootElement;
            var bosses = jsonObject
                .GetProperty("latestSnapshot")
                .GetProperty("data")
                .GetProperty("bosses");

            user.LatestSnapshot.UserMetrics.Bosses.BossDict = new Dictionary<string, Boss>();

            foreach (var bossElement in bosses.EnumerateObject())
            {
                
                var bossName = bossElement.Name;
                var bossData = bossElement.Value;

             
                var boss = new Boss
                {
                    Metric = bossData.GetProperty("metric").GetString(),
                    Kills = bossData.GetProperty("kills").GetInt32(),
                    Rank = bossData.GetProperty("rank").GetInt32(),
                    Ehb = bossData.GetProperty("ehb").GetDouble()
                };

              
                user.LatestSnapshot.UserMetrics.Bosses.BossDict[bossData.GetProperty("metric").GetString()] = boss;
            }
            return user;
        }
    }
}
