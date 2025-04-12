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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRS.Gateway.DataAccess;
using MiRs.Interactors.RuneHunter.Admin;

namespace MiRs.Interactors.RuneHunter
{
    /// <summary>
    /// The Interactor to Rune User Stats and Metrics.
    /// </summary>
    public class RegisterUserInteractor : RequestHandler<RegisterUserRequest, RegisterUserResponse>
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
        public RegisterUserInteractor(
            ILogger<RegisterUserInteractor> logger,
            IGenericSQLRepository<RHUser> rhUserRepository,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            rhUserRepository = _rhUserRepository;
            _appSettings = appSettings.Value;
        }

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
