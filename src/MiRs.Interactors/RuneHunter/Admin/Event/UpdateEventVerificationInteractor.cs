using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Logging;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.Admin.Event;
using MiRS.Gateway.DataAccess;

namespace MiRs.Interactors.RuneHunter.Admin.Event
{
    public class UpdateEventVerificationInteractor : RequestHandler<UpdateEventVerificationRequest, UpdateEventVerificationResponse>
    {
        private readonly IGenericSQLRepository<GuildEvent> _guildEventRepository;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEventVerificationInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public UpdateEventVerificationInteractor(
            ILogger<UpdateEventVerificationInteractor> logger,
            IGenericSQLRepository<GuildEvent> guildEventRepository,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _guildEventRepository = guildEventRepository;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to create a Guild team.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<UpdateEventVerificationResponse> HandleRequest(UpdateEventVerificationRequest request, UpdateEventVerificationResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.UpdateEventVerify, "Verifying Event Password for event id: {eventid} ", request.EventId);

            GuildEvent currentEvent = (await _guildEventRepository.Query(e => e.Id == request.EventId && e.GuildId == request.GuildId)).FirstOrDefault() ?? new GuildEvent();

            if (currentEvent.EventPassword == request.EventPassword)
            {
                result.Verfied = true;
            }
            else
            {
                result.Verfied = false;
            }

            return result;
        }
    }
}
