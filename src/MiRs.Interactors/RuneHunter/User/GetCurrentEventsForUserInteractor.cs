using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Logging;
using MiRS.Gateway.DataAccess;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.User;
using Microsoft.EntityFrameworkCore;
using MiRs.Domain.DTOs.RuneHunter;
using Microsoft.IdentityModel.Tokens;

namespace MiRs.Interactors.RuneHunter.User
{
    public class GetCurrentEventsForUserInteractor : RequestHandler<GetCurrentEventsForUserRequest, GetCurrentEventsForUserResponse>
    {
        private readonly IGenericSQLRepository<RHUser> _userRepository;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGuildTeamInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public GetCurrentEventsForUserInteractor(
            ILogger<GetCurrentEventsForUserInteractor> logger,
            IGenericSQLRepository<RHUser> userRepository,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to return all current events for user.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<GetCurrentEventsForUserResponse> HandleRequest(GetCurrentEventsForUserRequest request, GetCurrentEventsForUserResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.CurrentUserEvents, "Get current user events. User Id: {userId}", request.UserId);

            IList<RHUser> userWithEvents = (await _userRepository.QueryWithInclude(
                u => u.UserId == request.UserId,
                default,
                q => q.Include(utt => utt.UserToTeams!)
                        .ThenInclude(tt => tt.Team!)
                        .ThenInclude(ett => ett.EventTeams!)
                        .ThenInclude(e => e.Event))).ToList();

            IList<UserEvents> currentEventsForUser = userWithEvents
                .SelectMany(utt => utt.UserToTeams!)
                .SelectMany(ett => ett.Team!.EventTeams!)
                .Where(ett => ett.Event!.EventActive)
                .Select(e => new UserEvents
                {
                    Id = e.Event.Id,
                    GuildId = e.Event.GuildId,
                    Eventname = e.Event.Eventname,
                    EventStart = e.Event.EventStart,
                    EventEnd = e.Event.EventEnd,
                    EventTeam = new EventTeam
                    {
                        Id = e.Id,
                        TeamId = e.TeamId,
                        EventId = e.EventId
                    }
                }).ToList();

            result.UserCurrentEvents = currentEventsForUser.IsNullOrEmpty() ? new List<UserEvents>() : currentEventsForUser;

            return result;
        }
    }
}
