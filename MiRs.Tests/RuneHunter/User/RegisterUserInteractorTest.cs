using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiRs.Domain.Configurations;
using MiRs.Domain.Entities.RuneHunter;
using MiRs.Domain.Exceptions;
using MiRs.Interactors.RuneHunter.User;
using MiRs.Mediator.Models.RuneHunter.User;
using MiRS.Gateway.DataAccess;
using Moq;
using System.Linq.Expressions;

namespace MiRs.Tests.RuneHunter.User
{
    [TestClass]
    public class RegisterUserInteractorTest
    {
        private Mock<ILogger<RegisterUserInteractor>> _logger;
        private Mock<IGenericSQLRepository<RHUser>> _rhUserRepository;
        private Mock<IOptions<AppSettings>> _appSettings;
        private IQueryable<RHUser> _userData;

        /// <summary>
        /// Initialisation method.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            _logger = new Mock<ILogger<RegisterUserInteractor>>();
            _rhUserRepository = new Mock<IGenericSQLRepository<RHUser>>();
            _appSettings = new Mock<IOptions<AppSettings>>();

            ArrangeDBcontext();
        }

        /// <summary>
        /// A test that checks that invalid store numbers are validated correctly.
        /// </summary>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [TestMethod]
        public async Task when_given_valid_RhUser_object_create_user_then_return_new_user_object()
        {
            //Arrange
            RHUser userDetails = new RHUser()
            {
                UserId = 10001,
                Username = "NewUser",
                Runescapename = "NewRuneUser",
                CreatedDate = DateTime.Now,
            };

            _rhUserRepository.Setup(u => u.Query(It.IsAny<Expression<Func<RHUser, bool>>>(), null)).Returns(Task.FromResult(_userData));

            RegisterUserInteractor registerUserInteractor = new RegisterUserInteractor(_logger.Object, _rhUserRepository.Object, _appSettings.Object);

            RegisterUserRequest registerUserRequest = new RegisterUserRequest()
            {
                rhUserToBeCreated = userDetails,
            };

            //Act
            RegisterUserResponse response = await registerUserInteractor.Handle(registerUserRequest, CancellationToken.None);

            //Assert
            Assert.IsNotNull(response.RegisteredUser);
            Assert.AreEqual(userDetails, response.RegisteredUser);

        }

        /// <summary>
        /// A test that checks that invalid Discord User ID are validated correctly.
        /// </summary>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [TestMethod]
        public async Task when_user_id_is_invalid_then_return_bad_request()
        {
            //Arrange
            RHUser userDetails = new RHUser()
            {
                UserId = 0,
                Username = "InvalidUserId",
                Runescapename = "NewRuneUser",
                CreatedDate = DateTime.Now,
            };

            _rhUserRepository.Setup(u => u.Query(It.IsAny<Expression<Func<RHUser, bool>>>(), null)).Returns(Task.FromResult(_userData));

            RegisterUserInteractor registerUserInteractor = new RegisterUserInteractor(_logger.Object, _rhUserRepository.Object, _appSettings.Object);

            RegisterUserRequest registerUserRequest = new RegisterUserRequest()
            {
                rhUserToBeCreated = userDetails,
            };

            //Act
            //Assert
            await Assert.ThrowsExceptionAsync<BadRequestException>(async () => await registerUserInteractor.Handle(registerUserRequest, CancellationToken.None));
        }

        /// <summary>
        /// A test that checks that invalid Runescape name are validated correctly.
        /// </summary>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [TestMethod]
        public async Task when_Runescape_name_is_null_or_empty_then_return_bad_request()
        {
            //Arrange
            RHUser userDetails = new RHUser()
            {
                UserId = 30001,
                Username = "ValidUserId",
                Runescapename = "",
                CreatedDate = DateTime.Now,
            };

            _rhUserRepository.Setup(u => u.Query(It.IsAny<Expression<Func<RHUser, bool>>>(), null)).Returns(Task.FromResult(_userData));

            RegisterUserInteractor registerUserInteractor = new RegisterUserInteractor(_logger.Object, _rhUserRepository.Object, _appSettings.Object);

            RegisterUserRequest registerUserRequest = new RegisterUserRequest()
            {
                rhUserToBeCreated = userDetails,
            };

            //Act
            //Assert
            await Assert.ThrowsExceptionAsync<BadRequestException>(async () => await registerUserInteractor.Handle(registerUserRequest, CancellationToken.None));
        }

        /// <summary>
        /// A test that checks that invalid Discord Username are validated correctly.
        /// </summary>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        [TestMethod]
        public async Task when_Username_is_null_or_empty_then_return_bad_request()
        {
            //Arrange
            RHUser userDetails = new RHUser()
            {
                UserId = 50001,
                Username = "",
                Runescapename = "ValidRSN",
                CreatedDate = DateTime.Now,
            };

            _rhUserRepository.Setup(u => u.Query(It.IsAny<Expression<Func<RHUser, bool>>>(), null)).Returns(Task.FromResult(_userData));

            RegisterUserInteractor registerUserInteractor = new RegisterUserInteractor(_logger.Object, _rhUserRepository.Object, _appSettings.Object);

            RegisterUserRequest registerUserRequest = new RegisterUserRequest()
            {
                rhUserToBeCreated = userDetails,
            };

            //Act
            //Assert
            await Assert.ThrowsExceptionAsync<BadRequestException>(async () => await registerUserInteractor.Handle(registerUserRequest, CancellationToken.None));
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
