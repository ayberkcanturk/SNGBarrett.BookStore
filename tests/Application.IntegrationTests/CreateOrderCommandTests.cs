using System;
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

    public class CreateOrderCommandTests : TestBase
    {
        [Test]
        public void ShouldThrowValidationException()
        {
            var command = new CreateOrderCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateOrder()
        {
            const int customerId = 1;
            var orderId = await SendAsync(new CreateOrderCommand()
            {
                CustomerId = customerId,
                BookIds = new List<int>() { 1 }
            });

            var (scope, order) = Find<Order>(orderId);
            order.Should().NotBeNull();
            order.CustomerId.Should().Be(customerId);
            order.Customer.Should().NotBeNull();
            order.TotalPrice.Should().BeGreaterThan(0);
            order.TotalPrice.Should().Be(19.95m);
            order.OrderLines.Should().HaveCount(1);
            order.Cancelled.Should().BeFalse();
            order.CreatedBy.Should().Be(1);
            order.Created.Should().BeCloseTo(DateTime.Now, 10000);
            order.LastModifiedBy.Should().BeNull();
            order.LastModified.Should().BeNull();
            scope.Dispose();
        }
    }
}
