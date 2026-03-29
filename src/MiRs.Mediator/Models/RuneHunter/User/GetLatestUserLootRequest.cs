using MediatR;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.User
{
    public class GetLatestUserLootRequest : IRequest<GetLatestUserLootResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the Userid.
        /// </summary>
        public ulong UserId { get; set; }

        /// <summary>
        /// Validate method used to impliment Validations on request arguments.
        /// </summary>
        public void Validate()
        {
            if (UserId <= 0)
            {
                throw new BadRequestException("user invalid!");
            }
        }
    }
}
