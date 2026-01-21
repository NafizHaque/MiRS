using MiRs.Domain.DTOs.RuneHunter;

namespace MiRs.Mediator.Models.RuneHunter.User
{
    public class UserSearchResponse
    {
        public IEnumerable<GameUser> Users { get; set; }
    }
}
