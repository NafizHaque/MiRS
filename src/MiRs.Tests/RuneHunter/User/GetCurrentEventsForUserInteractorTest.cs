using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRS.Gateway.DataAccess;
using MiRs.Interactors.RuneHunter.User;
using Moq;
using Microsoft.Extensions.Logging;
using MiRs.Mediator.Models.RuneHunter.User;
using System.Linq.Expressions;
using Azure.Core;

namespace MiRs.Tests.RuneHunter.User
{
    [TestClass]
    public class GetCurrentEventsForUserInteractorTest
    {
        private Mock<ILogger<GetCurrentEventsForUserInteractor>> _logger;
        private Mock<IGenericSQLRepository<RHUser>> _rhUserRepository;
        private Mock<IOptions<AppSettings>> _appSettings;
        private IQueryable<RHUser> _userData;
        private IQueryable<GuildEvent> _guildEventData;
        private IQueryable<RHUserToTeam> _userToTeam;
        private IQueryable<GuildEventTeam> _guildEventTeams;
        private IQueryable<GuildTeam> _guildTeams;

        /// <summary>
        /// Initialisation method.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            _logger = new Mock<ILogger<GetCurrentEventsForUserInteractor>>();
            _rhUserRepository = new Mock<IGenericSQLRepository<RHUser>>();
            _appSettings = new Mock<IOptions<AppSettings>>();

            ArrangeDBcontext();
        }

        /// <summary>
        /// A test that checks that valid user id, guild id and Teamname is given to add user to team. 
        /// </summary>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [TestMethod]
        public async Task when_given_valid_userid_and_guildId_then_return_list_of_all_active_user_events_in_guild ()
        {
            //Arrange

            _rhUserRepository
                .Setup(r => r.GetAllEntitiesAsync(
                    It.IsAny<Expression<Func<RHUser, bool>>>(),
                    It.IsAny<CancellationToken>(),
                    It.IsAny<Func<IQueryable<RHUser>, IQueryable<RHUser>>>()))
                .ReturnsAsync(_userData);

            GetCurrentEventsForUserInteractor getCurrentEventsForUserInteractor = new GetCurrentEventsForUserInteractor(_logger.Object, _rhUserRepository.Object, _appSettings.Object);

            GetCurrentEventsForUserRequest getCurrentEventsForUserRequest = new GetCurrentEventsForUserRequest()
            {
                UserId = 1001999666
            };

            //Act
            GetCurrentEventsForUserResponse response = await getCurrentEventsForUserInteractor.Handle(getCurrentEventsForUserRequest, CancellationToken.None);

            //Assert
            Assert.IsTrue(response.UserCurrentEvents.Count() == _userData?
                .SelectMany(utt => utt.UserToTeams!)
                .SelectMany(ett => ett.Team!.EventTeams!)
                .Where(ett => ett.Event!.EventActive)
                .Select(e => e.Event!)
                .Count());

        }

        private void ArrangeDBcontext()
        {
            ulong userId = 1001999666;
            int eventId  = 2001;

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

            _guildTeams = new List<GuildTeam>()
            {
                new GuildTeam()
                {
                    Id = 1001,
                    GuildId = 2002002002,
                    EventTeams = _guildEventTeams.ToList()
                }
            }.AsQueryable();

            _userToTeam = new List<RHUserToTeam>()
            {
                new RHUserToTeam()
                {
                    UserId = 1001999666,
                    TeamId = 1001,
                    Team = _guildTeams.FirstOrDefault()
                }
            }.AsQueryable();

            _userData = new List<RHUser>()
            {
                new RHUser()
                {
                    UserId = userId,
                    Username = "Discord Fizz",
                    PreviousUsername = "Test Fizz",
                    Runescapename = "Fizze",
                    PreviousRunescapename = "Test Fizze",
                    CreatedDate = DateTime.Now,
                    UserToTeams = _userToTeam.Where(u => u.UserId == userId).ToList(),
                }
            }.AsQueryable();

            _guildEventTeams = new List<GuildEventTeam>()
            {
                new GuildEventTeam()
                {
                    EventId = 2001,
                    TeamId = 1001,
                    Event = _guildEventData.FirstOrDefault(u => u.Id == eventId),
                    Team = _guildTeams.FirstOrDefault(),
                }
            }.AsQueryable();
        }
    }
}
