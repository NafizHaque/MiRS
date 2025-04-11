using MiRs.Domain.Entities.User;
using MiRS.Gateway.RunescapeClient;
using MiRs.Mediator.Models.RuneUser;
using MiRs.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiRs.Mediator.Models.RuneHunter;

namespace MiRs.Interactors.RuneHunter
{
    /// <summary>
    /// The Interactor to Rune User Stats and Metrics.
    /// </summary>
    public class RegisterUserInteractor : RequestHandler<RegisterUserRequest, RegisterUserResponse>
    {
        /// <summary>
        /// Handles the request to create a user.
        /// </summary>
        /// <param name="request">The request to create user.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<RegisterUserResponse> HandleRequest(RegisterUserRequest request, RegisterUserResponse result, CancellationToken cancellationToken)
        {

            return result;

        }
    }
}
