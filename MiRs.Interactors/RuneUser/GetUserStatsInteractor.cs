
using MiRs.Domain.Entities.User;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneUser;
using MiRS.Gateway.RunescapeClient;

namespace MiRs.Interactors.RuneUser
{
    /// <summary>
    /// The Interactor to Rune User Stats and Metrics.
    /// </summary>
    public class GetUserStatsInteractor : RequestHandler<GetRuneUserRequest, GetRuneUserResponse>
    {
        private readonly IRuneClient _runeClient;

        public GetUserStatsInteractor(IRuneClient runeclient)
        {
            _runeClient = runeclient;
        }

        /// <summary>
        /// Handles the request to create a user.
        /// </summary>
        /// <param name="request">The request to create user.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<GetRuneUserResponse> HandleRequest(GetRuneUserRequest request, GetRuneUserResponse result, CancellationToken cancellationToken)
        {
            User response = await _runeClient.GetRuneUser(request.Username);

            result.UserRetrieved = response;

            return result;

        }
    }
}
