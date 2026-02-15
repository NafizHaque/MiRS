using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRs.Domain.DTOs.RuneHunter;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Logging;
using MiRs.Interactors.RuneHunter.Admin.Team;
using MiRs.Mediator;
using MiRs.Mediator.Models.RuneHunter.Admin.Event;
using MiRS.Gateway.DataAccess;

namespace MiRs.Interactors.RuneHunter.Admin.Event
{
    public class GetGuildTeamFromEventInteractor : RequestHandler<GetGuildTeamFromEventRequest, GetGuildTeamFromEventResponse>
    {

        private readonly IGenericSQLRepository<GuildEventTeam> _guildEventTeamRepository;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserInteractor"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="guildTeamRepository">The repo interface to table storage.</param>
        /// <param name="appSettings">The app settings.</param>
        public GetGuildTeamFromEventInteractor(
            ILogger<GetGuildTeamsInteractor> logger,
            IGenericSQLRepository<GuildEventTeam> guildEventTeamRepository,
            IOptions<AppSettings> appSettings)
            : base(logger)
        {
            _guildEventTeamRepository = guildEventTeamRepository;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to create a Guild team.
        /// </summary>
        /// <param name="request">The request to create Guild Team.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<GetGuildTeamFromEventResponse> HandleRequest(GetGuildTeamFromEventRequest request, GetGuildTeamFromEventResponse result, CancellationToken cancellationToken)
        {
            Logger.LogInformation((int)LoggingEvents.GetTeamsFromEvent, "Retrieving Teams from Event. Event Id: {eventId}", request.EventId);

            IList<GuildEventTeam> teamsfromEvent = (await _guildEventTeamRepository.QueryWithInclude(e => e.EventId == request.EventId, default, eg => eg.Include(egt => egt.Team).ThenInclude(utt => utt.UsersInTeam).ThenInclude(u => u.User))).ToList();

            result.GuildTeams = teamsfromEvent.Select(tfe => new GameTeam
            {
                Id = tfe.Team.Id,
                GuildId = tfe.Team.GuildId,
                TeamName = tfe.Team.TeamName,
                CreatedDate = tfe.Team.CreatedDate,
                UsersInTeam = tfe.Team.UsersInTeam
                    .Select(ut => new UserToTeam
                    {
                        Id = ut.Id,
                        UserId = ut.UserId,
                        TeamId = ut.TeamId,
                        User = new GameUser
                        {
                            UserId = ut.User.UserId,
                            Username = ut.User.Username,
                            PreviousUsername = ut.User.Username,
                            PreviousRunescapename = ut.User.PreviousRunescapename,
                            Runescapename = ut.User.Runescapename,
                            CreatedDate = ut.User.CreatedDate
                        }
                    })

            }).ToList();

            return result;
        }
    }
}
