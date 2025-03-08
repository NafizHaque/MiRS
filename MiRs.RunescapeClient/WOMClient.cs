using MiRS.Gateway.RunescapeClient;
using Flurl.Http;
using MiRs.Domain.Entities.User;
using MiRs.Domain.Entities.User.Skills.Skill_Object;
using System.Text.Json;
using MiRs.Interfaces.Helpers;
using MiRs.Domain.Mappers;

namespace MiRs.RunescapeClient
{
    /// <summary>
    /// The client for connection to the WOM API.
    /// </summary>
    public class WOMClient : IRuneClient
    {
        private readonly IJsonSeraliserDefaultOptions _jsonUtils;

        private readonly UserMapper _mapper;


        public WOMClient(IJsonSeraliserDefaultOptions jsonUtils, UserMapper mapper )
        {
            _jsonUtils = jsonUtils;
            _mapper = mapper;
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

            return _mapper.Map(jsonResponse);
        }

        /// <summary>
        /// The call to request to update and retrieve the Users latest data point.
        /// </summary>
        /// <param name="username">The Runescape RSN</param>
        /// <returns>The response status.</returns>
        public async Task<User> RequestUpdateRuneUser(string username)
        {
            string jsonResponse = await "https://api.wiseoldman.net/v2/"
                .WithHeader("Content-Type", "application/json")
                .AppendPathSegment($"players/{username}")
                .PostAsync()
                .ReceiveString();

            return _mapper.Map(jsonResponse);

        }
    }
}
