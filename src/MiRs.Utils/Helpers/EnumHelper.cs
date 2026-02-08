namespace MiRs.Utils.Helpers
{
    public class EnumHelper
    {
        public static T ParseOrDefault<T>(string value, T defaultValue = default)
        where T : struct, Enum
        {
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            if (!Enum.TryParse<T>(value, true, out T result))
                return defaultValue;

            return Enum.IsDefined(typeof(T), result)
                ? result
                : defaultValue;
        }
    }
}
