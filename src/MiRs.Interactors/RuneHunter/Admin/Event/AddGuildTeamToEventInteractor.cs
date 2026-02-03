using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Logging;
using MiRS.Gateway.DataAccess;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.Admin.Event;
using MiRs.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MiRs.Interactors.RuneHunter.Admin.Event
{
    public class AddGuildTeamToEventInteractor : RequestHandler<AddGuildTeamToEventRequest, AddGuildTeamToEventResponse>
    {
        private readonly IGenericSQLRepository<GuildEventTeam> _guildEventTeamRepository;
        private readonly IGenericSQLRepository<GuildTeam> _guildTeamRepository;
        private readonly IGenericSQLRepository<RHUserToTeam> _rhUserToTeamRepository;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddGuildTeamToEventInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to SQL storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public AddGuildTeamToEventInteractor(
        ILogger<UpdateTeamsToEventInteractor> logger,
        IGenericSQLRepository<GuildEventTeam> guildEventTeamRepository,
        IGenericSQLRepository<GuildTeam> guildTeamRepository,
        IGenericSQLRepository<RHUserToTeam> rhUserToTeamRepository,
        IOptions<AppSettings> appSettings)
        : base(logger)
        {
            _guildEventTeamRepository = guildEventTeamRepository;
            _guildTeamRepository = guildTeamRepository;
            _rhUserToTeamRepository = rhUserToTeamRepository;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to create a Guild team.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<AddGuildTeamToEventResponse> HandleRequest(AddGuildTeamToEventRequest request, AddGuildTeamToEventResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.CreateGuildTeam, "Linking Team to Event. Team: {teamname}, Event Id: {eventId} ", request.NewTeamToBeCreated.TeamName, request.EventId);

            GuildTeam guildTeamToEvent;

            IList<GuildEventTeam> teamsfromEvent = (await _guildEventTeamRepository.GetAllEntitiesAsync(e => e.EventId == request.EventId, default, eg => eg.Include(egt => egt.Team).ThenInclude(utt => utt.UsersInTeam).ThenInclude(u => u.User))).ToList();

            if (teamsfromEvent.Where(t => t.Team.TeamName == request.NewTeamToBeCreated.TeamName && t.Team.GuildId == request.NewTeamToBeCreated.GuildId).Any())
            {
                throw new BadRequestException("Team is already linked to event!");
            }

            HashSet<ulong> newTeamUserIds = request.NewTeamToBeCreated.UsersInTeam?
                .Select(x => x.UserId)
                .ToHashSet()
                ?? new HashSet<ulong>();

            if (request.AddExistingTeamToggle)
            {
                guildTeamToEvent = request.NewTeamToBeCreated;

                IList<RHUserToTeam> currentTeamUsers = (await _rhUserToTeamRepository.Query(ut => ut.TeamId == request.NewTeamToBeCreated.Id)).ToList();

                IList<RHUserToTeam> NewPlayersToUpdated = newTeamUserIds
                    .Select(userId => new RHUserToTeam
                    {
                        TeamId = guildTeamToEvent.Id,
                        UserId = userId
                    })
                    .ToList();

                HashSet<ulong> existingUserIds = currentTeamUsers
                    .Select(x => x.UserId)
                    .ToHashSet();

                HashSet<ulong> incomingUserIds = NewPlayersToUpdated
                    .Select(x => x.UserId)
                    .ToHashSet()
                    ?? new HashSet<ulong>();

                IList<RHUserToTeam> toRemove = currentTeamUsers
                    .Where(x => !incomingUserIds.Contains(x.UserId))
                    .ToList();

                await _rhUserToTeamRepository.AddRangeAsync(NewPlayersToUpdated);
                await _rhUserToTeamRepository.DeleteManyAsync(toRemove);

            }
            else
            {
                guildTeamToEvent = await _guildTeamRepository.AddAsync(new GuildTeam
                {
                    GuildId = request.NewTeamToBeCreated.GuildId,
                    TeamName = request.NewTeamToBeCreated.TeamName,
                    CreatedDate = DateTimeOffset.Now,
                });

                IList<RHUserToTeam> NewPlayersToAdd = newTeamUserIds
                    .Select(userId => new RHUserToTeam
                    {
                        TeamId = guildTeamToEvent.Id,
                        UserId = userId
                    })
                    .ToList();

                await _rhUserToTeamRepository.AddRangeAsync(NewPlayersToAdd);
            }

            await _guildEventTeamRepository.AddAsync(
                new GuildEventTeam
                {
                    EventId = request.EventId,
                    TeamId = guildTeamToEvent.Id,
                });

            return result;
        }
    }
}
