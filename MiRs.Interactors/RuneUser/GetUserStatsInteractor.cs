
using MiRs.Mediator.Models.RuneUser;
using MiRs.Mediator;

namespace MiRs.Interactors.RuneUser
{
    /// <summary>
    /// The Interactor to Rune User Stats and Metrics.
    /// </summary>
    public class GetUserStatsInteractor : RequestHandler<GetRuneUserRequest, GetRuneUserResponse>
    {
        public GetUserStatsInteractor()
        {
            
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
            return new GetRuneUserResponse();
            
        }
    }
}
