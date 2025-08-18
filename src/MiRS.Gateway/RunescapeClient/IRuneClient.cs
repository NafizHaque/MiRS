using MiRs.Domain.Entities.User;

namespace MiRS.Gateway.RunescapeClient
{
    public interface IRuneClient
    {
        Task<User> GetRuneUser(string username);

        Task<User> RequestUpdateRuneUser(string username);
    }
}
