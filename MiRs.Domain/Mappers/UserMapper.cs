using MiRs.Domain.Entities.User;
using MiRs.Domain.Entities.User.Skills.Skill_Object;
using MiRs.Interfaces.Helpers;
using System.Text.Json;

namespace MiRs.Domain.Mappers
{
    public class UserMapper
    {
        private readonly IJsonSeraliserDefaultOptions _jsonUtils;

        public UserMapper(IJsonSeraliserDefaultOptions jsonUtils)
        {
            _jsonUtils = jsonUtils;
        }

        /// <summary>
        /// Maps data to the User object.
        /// </summary>
        /// <param name="userData">userFileData object.</param>
        /// <returns>UserTableEntity object.</returns>
        public User Map(string jsonresponse)
        {

            if (string.IsNullOrEmpty(jsonresponse))
            {
                return new User();
            }
            ;

            User user = _jsonUtils.Deserialize<User>(jsonresponse) ?? new User();

            JsonElement jsonObject = JsonDocument.Parse(jsonresponse).RootElement;
            JsonElement bosses = jsonObject
                .GetProperty("latestSnapshot")
                .GetProperty("data")
                .GetProperty("bosses");

            user.LatestSnapshot.UserMetrics.Bosses.BossDict = new Dictionary<string, Boss>();

            foreach (JsonProperty bossElement in bosses.EnumerateObject())
            {

                string bossName = bossElement.Name;
                JsonElement bossData = bossElement.Value;

                Boss boss = new Boss
                {
                    Metric = bossData.GetProperty("metric").GetString() ?? string.Empty,
                    Kills = bossData.GetProperty("kills").GetInt32(),
                    Rank = bossData.GetProperty("rank").GetInt32(),
                    Ehb = bossData.GetProperty("ehb").GetDouble()
                };

                user.LatestSnapshot.UserMetrics.Bosses.BossDict[bossData.GetProperty("metric").GetString() ?? string.Empty] = boss;
            }
            return user;
        }
    }
}
