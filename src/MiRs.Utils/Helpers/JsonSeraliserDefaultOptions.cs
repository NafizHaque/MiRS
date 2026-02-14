using MiRs.Helpers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MiRs.Utils.Helpers
{
    public class JsonSeraliserDefaultOptions : IJsonSeraliserDefaultOptions
    {
        private JsonSerializerOptions DefaultOptions { get; }

        public JsonSeraliserDefaultOptions()
        {
            DefaultOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }

        public TValue? Deserialize<TValue>(string document, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Deserialize<TValue>(document, options ?? DefaultOptions);
        }
    }
}
