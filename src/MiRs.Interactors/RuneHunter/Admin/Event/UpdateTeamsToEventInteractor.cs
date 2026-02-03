using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.Admin.Event;
using MiRS.Gateway.DataAccess;

namespace MiRs.Interactors.RuneHunter.Admin.Event
{
    public class UpdateTeamsToEventInteractor : RequestHandler<UpdateTeamsToEventRequest, UpdateTeamsToEventResponse>
    {
        private readonly IGenericSQLRepository<GuildEventTeam> _guildEventTeamRepository;
        private readonly IGenericSQLRepository<RHUserToTeam> _rhUserToTeamRepository;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTeamsToEventInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public UpdateTeamsToEventInteractor(
            ILogger<UpdateTeamsToEventInteractor> logger,
            IGenericSQLRepository<GuildEventTeam> guildEventTeamRepository,
            IGenericSQLRepository<RHUserToTeam> rhUserToTeamRepository,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _guildEventTeamRepository = guildEventTeamRepository;
            _rhUserToTeamRepository = rhUserToTeamRepository;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to unlink Team from Event.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<UpdateTeamsToEventResponse> HandleRequest(UpdateTeamsToEventRequest request, UpdateTeamsToEventResponse result, CancellationToken cancellationToken)
        {
            //Logger.LogInformation((int)LoggingEvents.CreateGuildTeam, "Linking Team to Event. Team Id: {teamId}, Event Id: {eventId} ", request.TeamId, request.EventId);
            GuildTeam guildTeamToEvent;

            IList<GuildEventTeam> teamsfromEvent = (await _guildEventTeamRepository.GetAllEntitiesAsync(e => e.EventId == request.EventId, default, eg => eg.Include(egt => egt.Team).ThenInclude(utt => utt.UsersInTeam).ThenInclude(u => u.User))).ToList();

            IList<GuildTeam> teams = teamsfromEvent?
                .Select(t => t.Team!)
                .ToList()
                ?? new List<GuildTeam>();

            foreach (GuildTeam incomingTeam in request.CurrentTeamsToBeUpdated)
            {
                GuildTeam dbTeam = teams.First(t => t.Id == incomingTeam.Id);

                IList<RHUserToTeam> existingUsersToTeam = dbTeam.UsersInTeam?.ToList() ?? new List<RHUserToTeam>();

                HashSet<ulong> existingUserIds = existingUsersToTeam
                    .Select(x => x.UserId)
                    .ToHashSet();

                HashSet<ulong> incomingUserIds = incomingTeam.UsersInTeam?
                    .Select(x => x.UserId)
                    .ToHashSet()
                    ?? new HashSet<ulong>();

                IList<RHUserToTeam> toAdd = incomingUserIds
                    .Except(existingUserIds)
                    .Select(userId => new RHUserToTeam
                    {
                        TeamId = dbTeam.Id,
                        UserId = userId
                    })
                    .ToList();

                IList<RHUserToTeam> toRemove = existingUsersToTeam
                    .Where(x => !incomingUserIds.Contains(x.UserId))
                    .ToList();

                await _rhUserToTeamRepository.AddRangeAsync(toAdd);
                await _rhUserToTeamRepository.DeleteManyAsync(toRemove);
            }

            return result;
        }
    }
}
