using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter;
using MiRs.Mediator.Models.RuneHunter.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Interactors.RuneHunter.Admin
{
    public class CreateGuildTeamInteractor : RequestHandler<CreateGuildTeamRequest, CreateGuildTeamResponse>
    {
        /// <summary>
        /// Handles the request to create a user.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<CreateGuildTeamResponse> HandleRequest(CreateGuildTeamRequest request, CreateGuildTeamResponse result, CancellationToken cancellationToken)
        {

            return result;

        }
    }
}
