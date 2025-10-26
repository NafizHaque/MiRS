using MiRs.Mediator.Models.RuneHunter.Game;
using MiRs.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;
using MiRs.Domain.Logging;
using MiRS.Gateway.DataAccess;
using System.Text.RegularExpressions;
using MiRs.Domain.Entities.User;
using MiRs.Mediator.Models.RuneHunter.User;
using MediatR;

namespace MiRs.Interactors.RuneHunter.Game
{
    public class ProcessUserLootInteractor : RequestHandler<ProcessUserLootRequest, ProcessUserLootResponse>
    {
        private readonly IGenericSQLRepository<RHUserRawLoot> _rhUserRawLoot;
        private readonly IGenericSQLRepository<RHUser> _rhUserRepository;
        private readonly AppSettings _appSettings;
        private readonly ISender _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessUserLootInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public ProcessUserLootInteractor(
            ILogger<ProcessUserLootInteractor> logger,
            IGenericSQLRepository<RHUserRawLoot> rhUserRawLoot,
            IGenericSQLRepository<RHUser> rhUserRepository,
            ISender mediator,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _rhUserRawLoot = rhUserRawLoot;
            _rhUserRepository = rhUserRepository;
            _appSettings = appSettings.Value;
            _mediator = mediator;
        }

        /// <summary>
        /// Handles the request to process user loot.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<ProcessUserLootResponse> HandleRequest(ProcessUserLootRequest request, ProcessUserLootResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.GameProcessLoot, "Proocessing User Loot.");

            IEnumerable<RHUserRawLoot> unprocessedUserLoot = await _rhUserRawLoot.Query(l => !l.Processed);

            IEnumerable<IGrouping<ulong, RHUserRawLoot>> groupedLoot = unprocessedUserLoot.GroupBy(l => l.UserId);

            foreach (IGrouping<ulong, RHUserRawLoot> group in groupedLoot)
            {
                GetCurrentEventsForUserResponse currentUserEvents =  await _mediator.Send(new GetCurrentEventsForUserRequest { UserId = group.Key });

                foreach (RHUserRawLoot loot in group)
                {
                    Console.WriteLine($"  LootId: {loot.Id}, Processed: {loot.Processed}");
                }
            }

            Logger.LogInformation((int)LoggingEvents.GameProcessLoot, "Loot ({totalLoot}) to be processed.", unprocessedUserLoot.Count());

            return result;
        }
    }
}
