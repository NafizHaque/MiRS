using MiRS.Gateway.RunescapeClient;
using Flurl.Http;
using MiRs.Domain.Entities.User;

namespace MiRs.RunescapeClient
{
    /// <summary>
    /// The client for connection to the WOM API.
    /// </summary>
    public class WOMClient : IRuneClient
    {

        /// <summary>
        /// The call to get user in RS via the WOM API.
        /// </summary>
        /// <param name="username">The Runescape RSN</param>
        /// <returns>The response status.</returns>
        public async Task<User> GetRuneUser(string username)
        {
            return await "https://api.wiseoldman.net/v2/"
                .WithHeader("Content-Type", "application/json")
                .AppendPathSegment($"players/{username}")
                .GetJsonAsync<User>();
        }
    }
}
