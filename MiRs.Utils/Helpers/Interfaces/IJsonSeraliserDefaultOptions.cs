using System.Text.Json;

namespace MiRs.Utils.Helpers.Interfaces
{
    public interface IJsonSeraliserDefaultOptions
    {
        TValue? Deserialize<TValue>(string document, JsonSerializerOptions? options = null);

    }
}
