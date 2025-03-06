namespace MiRS.Gateway.RunescapeClient
{
    public interface IRuneClient
    {
        Task<string> GetRuneUser(string username);
    }
}
