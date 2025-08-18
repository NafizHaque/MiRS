using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRS.Gateway.DataAccess;
using MiRs.Interactors.RuneHunter.User;
using Moq;
using Microsoft.Extensions.Logging;

namespace MiRs.Tests.RuneHunter.User
{
    [TestClass]
    public class JoinTeamInteractorTest
    {
        private Mock<ILogger<JoinTeamInteractor>> _logger;
        private Mock<IGenericSQLRepository<RHUserToTeam>> _rhUserToTeamRepository;
        private Mock<IGenericSQLRepository<GuildTeam>> _guildTeamRepository;
        private Mock<IOptions<AppSettings>> _appSettings;
        private IQueryable<RHUser> _userData;

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

        private void ArrangeDBcontext()
        {
            _userData = new List<RHUser>()
            {
                new RHUser()
                {
                    UserId = 1001999666,
                    Username = "Discord Fizz",
                    PreviousUsername = "Test Fizz",
                    Runescapename = "Fizze",
                    PreviousRunescapename = "Test Fizze",
                    CreatedDate = DateTime.Now,
                }
            }.AsQueryable();
        }
    }
}
