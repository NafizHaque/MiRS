using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRS.Gateway.DataAccess;
using MiRs.Interactors.RuneHunter.User;
using Moq;
using Microsoft.Extensions.Logging;
using MiRs.Interactors.RuneHunter.Admin;
using MiRs.Mediator.Models.RuneHunter.User;
using System.Linq.Expressions;
using MiRs.Mediator.Models.RuneHunter.Admin;

namespace MiRs.Tests.RuneHunter.Admin
{
    [TestClass]
    public class AddGuildTeamToEventInteractorTest
    {
        private Mock<ILogger<AddGuildTeamToEventInteractor>> _logger;
        private Mock<IGenericSQLRepository<GuildEventTeam>> _guildTeamEventRepository;
        private Mock<IGenericSQLRepository<GuildTeam>> _guildTeamRepository;
        private Mock<IGenericSQLRepository<GuildEvent>> _guildEventRepository;
        private Mock<IOptions<AppSettings>> _appSettings;

        private IQueryable<GuildEventTeam> _guildEventTeams;
        private IQueryable<GuildTeam> _guildTeamsData;
        private IQueryable<GuildEvent> _guildEventData;

        /// <summary>
        /// Initialisation method.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            _logger = new Mock<ILogger<AddGuildTeamToEventInteractor>>();

            _guildTeamEventRepository = new Mock<IGenericSQLRepository<GuildEventTeam>>();
            _guildTeamRepository = new Mock<IGenericSQLRepository<GuildTeam>>();
            _guildEventRepository = new Mock<IGenericSQLRepository<GuildEvent>>();

            _appSettings = new Mock<IOptions<AppSettings>>();

            ArrangeDBcontext();
        }

        /// <summary>
        /// A test that checks that valid user id, guild id and Teamname is given to add user to team. 
        /// </summary>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [TestMethod]
        public async Task when_given_valid_event_and_team_id_then_register_team_to_event()
        {
            //Arrange

            _guildTeamRepository.Setup(u => u.Query(It.IsAny<Expression<Func<GuildTeam, bool>>>(), null)).Returns(Task.FromResult(_guildTeamsData));

            _guildEventRepository.Setup(u => u.Query(It.IsAny<Expression<Func<GuildEvent, bool>>>(), null)).Returns(Task.FromResult(_guildEventData));

            _guildTeamEventRepository.Setup(u => u.Query(It.IsAny<Expression<Func<GuildEventTeam, bool>>>(), null)).Returns(Task.FromResult(_guildEventTeams));

            AddGuildTeamToEventInteractor addGuildTeamToEventInteractor = new AddGuildTeamToEventInteractor(
                                                                                _logger.Object, 
                                                                                _guildTeamEventRepository.Object,
                                                                                _guildTeamRepository.Object,
                                                                                _guildEventRepository.Object,
                                                                                _appSettings.Object);

            AddGuildTeamToEventRequest addGuildTeamToEventRequest = new AddGuildTeamToEventRequest()
            {
                EventId = 2001,
                TeamId = 1001
            };

            //Act
            AddGuildTeamToEventResponse response = await addGuildTeamToEventInteractor.Handle(addGuildTeamToEventRequest, CancellationToken.None);

            //Assert
            _guildTeamEventRepository.Verify(
                r => r.AddAsync(It.IsAny<GuildEventTeam>(),
                CancellationToken.None),
                Times.Once,
                "Expected AddAsync to be called once when handling Add team to event request"
            );
        }

        private void ArrangeDBcontext()
        {
            int eventId = 2001;

            _guildEventData = new List<GuildEvent>()
            {
                new GuildEvent()
                {
                    Id = 2001,
                    GuildId = 2002002002,
                    Eventname = "Summer Event",
                    EventActive = true,
                    CreatedDate = DateTimeOffset.UtcNow.AddDays(-10),
                    EventStart = DateTimeOffset.UtcNow,
                    EventEnd = DateTimeOffset.UtcNow.AddDays(10),

                },
                new GuildEvent()
                {
                    Id = 2002,
                    GuildId = 2002002002,
                    Eventname = "Winter Event",
                    EventActive = false,
                    CreatedDate = DateTimeOffset.UtcNow.AddDays(-10),
                    EventStart = DateTimeOffset.UtcNow.AddMonths(3),
                    EventEnd = DateTimeOffset.UtcNow.AddMonths(3).AddDays(10),
                },
            }.AsQueryable();

            _guildEventTeams = new List<GuildEventTeam>()
            {
                new GuildEventTeam()
                {
                    EventId = 2001,
                    TeamId = 1001,
                    Event = _guildEventData.FirstOrDefault(u => u.Id == eventId),
                }
            }.AsQueryable();

            _guildTeamsData = new List<GuildTeam>()
            {
                new GuildTeam()
                {
                    Id = 1001,
                    GuildId = 2002002002,
                    EventTeams = _guildEventTeams.ToList()
                }
            }.AsQueryable();

        }
    }
}
