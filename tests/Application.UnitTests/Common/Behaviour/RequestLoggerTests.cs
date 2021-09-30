using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;

using SNGBarrett.BookStore.Application.Commands;
using SNGBarrett.BookStore.Application.SharedKernel.Behaviour;
using SNGBarrett.BookStore.Application.SharedKernel.Interfaces;

namespace SNGBarrett.BookStore.Application.UnitTests.Common.Behaviour
{
    public class RequestLoggerTests
    {
        private readonly Mock<ILogger<CreateOrderCommand>> _logger;
        private readonly Mock<ICurrentUserService> _currentUserService;

        public RequestLoggerTests()
        {
            _logger = new Mock<ILogger<CreateOrderCommand>>();

            _currentUserService = new Mock<ICurrentUserService>();
        }

        [Test]
        public async Task ShouldCallGetCurrentUserServiceUserForOnce()
        {
            _currentUserService.Setup(x => x.UserId).Returns(1);

            var requestLogger = new LoggingBehaviour<CreateOrderCommand>(_logger.Object, _currentUserService.Object);

            await requestLogger.Process(new CreateOrderCommand { CustomerId = 1, BookIds = new List<int>() { 1 } }, new CancellationToken());

            _currentUserService.VerifyGet(i => i.UserId, Times.Once);
        }
    }
}
