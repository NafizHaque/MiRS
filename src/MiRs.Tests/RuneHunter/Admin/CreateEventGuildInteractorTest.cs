using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRS.Gateway.DataAccess;
using MiRs.Interactors.RuneHunter.User;
using MiRs.Mediator.Models.RuneHunter.User;
using Moq;
using Microsoft.Extensions.Logging;
using MiRs.Interactors.RuneHunter.Admin;
using MiRs.Mediator.Models.RuneHunter.Admin;

namespace MiRs.Tests.RuneHunter.Admin
{
    [TestClass]
    public class CreateEventGuildInteractorTest
    {
        private Mock<ILogger<CreateGuildEventInteractor>> _logger;
        private Mock<IGenericSQLRepository<GuildEvent>> _guildEventRepository;
        private Mock<IOptions<AppSettings>> _appSettings;

        /// <summary>
        /// Initialisation method.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            _logger = new Mock<ILogger<CreateGuildEventInteractor>>();
            _guildEventRepository = new Mock<IGenericSQLRepository<GuildEvent>>();
            _appSettings = new Mock<IOptions<AppSettings>>();

            ArrangeDBcontext();
        }

        /// <summary>
        /// A test that checks that valid event object, create guild object and return the details. 
        /// </summary>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [TestMethod]
        public async Task when_given_valid_event_object_create_event_then_return_new_event_object()
        {
            //Arrange

            GuildEvent testEvent = new GuildEvent
            {
                Id = 1,
                GuildId = 2002002002,
                Eventname = "Event To Be Tested",
                EventStart = DateTimeOffset.UtcNow,
                EventEnd = DateTimeOffset.UtcNow.AddDays(10),
            };

            CreateGuildEventInteractor createGuildEventInteractor = new CreateGuildEventInteractor(_logger.Object, _guildEventRepository.Object, _appSettings.Object);

            CreateEventInGuildRequest joinTeamRequest = new CreateEventInGuildRequest()
            {
                GuildEventToBeCreated = testEvent,
            };

            //Act
            CreateEventInGuildResponse response = await createGuildEventInteractor.Handle(joinTeamRequest, CancellationToken.None);

            //Assert
            _guildEventRepository.Verify(
                r => r.AddAsync(It.IsAny<GuildEvent>(),
                CancellationToken.None),
                Times.Once,
                "Expected AddAsync to be called once when handling Add event request"
);
        }

        private void ArrangeDBcontext()
        {
        }
    }
}
