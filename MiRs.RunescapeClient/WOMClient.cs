using MiRS.Gateway.RunescapeClient;
using Flurl.Http;
using MiRs.Domain.Entities.User;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

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
            try
            {
                // Make the GET request
                var response = await "https://api.wiseoldman.net/v2/"
                    .WithHeader("Content-Type", "application/json")
                    .AppendPathSegment($"players/{username}")
                    .GetAsync(); // Use GetAsync to manually handle deserialization

                // Read the raw response as a string
                var rawResponse = await response.ResponseMessage.Content.ReadAsStringAsync();

                Console.WriteLine(rawResponse);

                // Deserialize the JSON manually with case-insensitive settings
                var user = JsonConvert.DeserializeObject<User>(rawResponse, new JsonSerializerSettings
                {
                    // Make deserialization case-insensitive
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy() // Ensure case-insensitivity
                    },
                    // Optionally handle missing members gracefully
                    MissingMemberHandling = MissingMemberHandling.Ignore
                });

                return user;
            }
            catch (FlurlHttpException ex)
            {
                // Handle HTTP errors (e.g., player not found, server error)
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return null;
            }
        }
    }
 
}
