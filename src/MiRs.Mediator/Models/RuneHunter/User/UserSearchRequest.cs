using MediatR;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.User
{
    public class UserSearchRequest : IRequest<UserSearchResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the searchkey.
        /// </summary>
        public string Searchkey { get; set; } = string.Empty;

        /// <summary>
        /// Validate method used to impliment Validations on request arguments.
        /// </summary>
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Searchkey))
            {
                throw new BadRequestException("search key is empty!");
            }
        }
    }
}
