using System.Collections.Generic;
using System.Threading.Tasks;

using FluentAssertions;

using NUnit.Framework;

using SNGBarrett.BookStore.Application.Commands;
using SNGBarrett.BookStore.Application.SharedKernel.Exceptions;
using SNGBarrett.BookStore.Domain.Entities;

namespace SNGBarrett.BookStore.Application.IntegrationTests
{
    using static Testing;

    public class CancelOrderCommandTests : TestBase
    {
        [Test]
        public void ShouldThrowValidationException()
        {
            var command = new CancelOrderCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldThrowExceptionWhenOrderNotFound()
        {
            var command = new CancelOrderCommand()
            {
                OrderId = 5
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldCancelWhenCancelOrderCommandHandled()
        {
            var orderId = await SendAsync(new CreateOrderCommand()
            {
                CustomerId = 1,
                BookIds = new List<int>() { 1 }
            });

            await SendAsync(new CancelOrderCommand()
            {
                OrderId = orderId
            });

            var order = await FindAsync<Order>(orderId);

            order.Cancelled.Should().BeTrue();
        }
    }
}