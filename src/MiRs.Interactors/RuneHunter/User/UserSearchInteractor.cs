using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.DTOs.RuneHunter;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Logging;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.User;
using MiRS.Gateway.DataAccess;

namespace MiRs.Interactors.RuneHunter.User
{
    public class UserSearchInteractor : RequestHandler<UserSearchRequest, UserSearchResponse>
    {
        private readonly IGenericSQLRepository<RHUser> _rhUserRepository;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUserInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="userRepository">The repo interface to table storage.</param>
        /// <param name="configRepository">The repo interface to table storage for config data.</param>
        /// <param name="appSettings">The app settings.</param>
        public UserSearchInteractor(
            ILogger<JoinTeamInteractor> logger,
            IGenericSQLRepository<RHUser> rhUserRepository,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _rhUserRepository = rhUserRepository;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to get searched users.
        /// </summary>
        /// <param name="request">The request to create user.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<UserSearchResponse> HandleRequest(UserSearchRequest request, UserSearchResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.UserSearch, "Searching user by search key: {search}", request.Searchkey);

            IEnumerable<RHUser> users = (await _rhUserRepository.Query(u => u.Runescapename.StartsWith(request.Searchkey)));

            result.Users = users.OrderBy(u => u.Runescapename.Length).ThenBy(u => u.Runescapename).Take(25)
                .Select(u => new GameUser
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    PreviousUsername = u.PreviousUsername,
                    Runescapename = u.Runescapename,
                    PreviousRunescapename = u.Runescapename,
                    CreatedDate = u.CreatedDate,
                });

            return result;
        }
    }
}
