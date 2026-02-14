using System.Text.Json;

namespace MiRs.Helpers
{
    public interface IJsonSeraliserDefaultOptions
    {
        TValue? Deserialize<TValue>(string document, JsonSerializerOptions? options = null);

    }
}
