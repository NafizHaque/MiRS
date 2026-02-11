using MediatR;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter.Game
{
    public class GetEventTeamProgressForUserRequest : IRequest<GetEventTeamProgressForUserResponse>, IValidatable
    {
        public ulong UserId { get; set; }

        public ulong GuildId { get; set; }

        public void Validate()
        {
            if (UserId <= 0)
            {
                throw new BadRequestException("User id is null or missing!");
            }

            if (GuildId <= 0)
            {
                throw new BadRequestException("Guild id is null or missing!");
            }
        }
    }
}
