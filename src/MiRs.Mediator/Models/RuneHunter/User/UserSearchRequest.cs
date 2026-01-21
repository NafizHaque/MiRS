using MediatR;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.User
{
    public class UserSearchRequest : IRequest<UserSearchResponse>, IValidatable
    {
        public string Searchkey { get; set; } = string.Empty;

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Searchkey))
            {
                throw new BadRequestException("search key is empty!");
            }
        }
    }
}
