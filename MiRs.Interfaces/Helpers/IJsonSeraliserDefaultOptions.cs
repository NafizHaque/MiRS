using System.Text.Json;

namespace MiRs.Interfaces.Helpers
{
    public interface IJsonSeraliserDefaultOptions
    {
        TValue? Deserialize<TValue>(string document, JsonSerializerOptions? options = null);

    }
}
