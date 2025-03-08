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
            var user = _jsonUtils.Deserialize<User>(jsonresponse);


            var jsonObject = JsonDocument.Parse(jsonresponse).RootElement;
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
