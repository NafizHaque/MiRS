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

namespace MiRs.Tests.RuneHunter.User
{
    [TestClass]
    public class JoinTeamInteractorTest
    {
        private Mock<ILogger<JoinTeamInteractor>> _logger;
        private Mock<IGenericSQLRepository<RHUserToTeam>> _rhUserToTeamRepository;
        private Mock<IGenericSQLRepository<GuildTeam>> _guildTeamRepository;
        private Mock<IOptions<AppSettings>> _appSettings;
        private IQueryable<RHUserToTeam> _userToTeamData;
        private IQueryable<GuildTeam> _guildTeamData;

        /// <summary>
        /// Initialisation method.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            _logger = new Mock<ILogger<JoinTeamInteractor>>();
            _rhUserToTeamRepository = new Mock<IGenericSQLRepository<RHUserToTeam>>();
            _guildTeamRepository = new Mock<IGenericSQLRepository<GuildTeam>>();
            _appSettings = new Mock<IOptions<AppSettings>>();

            ArrangeDBcontext();
        }

        /// <summary>
        /// A test that checks that valid user id, guild id and Teamname is given to add user to team. 
        /// </summary>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [TestMethod]
        public async Task when_given_valid_RhUser_object_create_user_then_return_new_user_object()
        {
            //Arrange
            _rhUserToTeamRepository.Setup(u => u.Query(It.IsAny<Expression<Func<RHUserToTeam, bool>>>(), null)).Returns(Task.FromResult(_userToTeamData));

            _guildTeamRepository.Setup(u => u.Query(It.IsAny<Expression<Func<GuildTeam, bool>>>(), null)).Returns(Task.FromResult(_guildTeamData));

            JoinTeamInteractor joinTeamInteractor = new JoinTeamInteractor(_logger.Object, _rhUserToTeamRepository.Object, _guildTeamRepository.Object, _appSettings.Object);

            JoinTeamRequest joinTeamRequest = new JoinTeamRequest()
            {
                UserId = 1001001001,
                GuildId = 2002002002,
                Teamname = "TestTeam"

            };

            //Act
            JoinTeamResponse response = await joinTeamInteractor.Handle(joinTeamRequest, CancellationToken.None);

            //Assert
            //Assert - verify AddAsync was called
            _rhUserToTeamRepository.Verify(
                r => r.AddAsync(It.IsAny<RHUserToTeam>(),
                CancellationToken.None),
                Times.Once,
                "Expected AddAsync to be called once when handling join team request"
            );

        }

        private void ArrangeDBcontext()
        {
            _userToTeamData = new List<RHUserToTeam>()
            {
                new RHUserToTeam()
                {
                    Id = 1000,
                    UserId = 1001999666,
                    TeamId = 2001,
                },
                new RHUserToTeam()
                {
                    Id = 1010,
                    UserId = 3004441941,
                    TeamId = 3001,
                },
                new RHUserToTeam()
                {
                    Id = 1020,
                    UserId = 207483916,
                    TeamId = 2001,
                }
            }.AsQueryable();

            _guildTeamData = new List<GuildTeam>()
            {
                new GuildTeam()
                {
                    GuildId = 2002002002,
                    TeamName = "TestTeam",
                    CreatedDate = DateTime.UtcNow,
                },
                new GuildTeam()
                {
                    GuildId = 2002003003,
                    TeamName = "Boss Team X1",
                    CreatedDate = DateTime.UtcNow,
                },
            }.AsQueryable();
        }
    }
}
