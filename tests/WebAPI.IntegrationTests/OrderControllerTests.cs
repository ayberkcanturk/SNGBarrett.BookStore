using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using Newtonsoft.Json;

using NUnit.Framework;

using SNGBarrett.BookStore.Application.Commands;
using SNGBarrett.BookStore.WebAPI;

namespace WebAPI.IntegrationTests
{
    public class Tests
    {
        private HttpClient _client;

        private const string CreateOrderUri = "/api/order";


        [OneTimeSetUp]
        public void Setup()
        {
            var server = new TestHost<Startup>();
            _client = server.CreateClient();
        }

        [Test]
        public async Task CreateOrder()
        {
            var customerId = 1;
            var bookIds = new List<int>() {1};

            var httpResponseMessage = await CreateOrder(customerId, bookIds);

            httpResponseMessage.IsSuccessStatusCode.Should().BeTrue();
        }

        [Test]
        public async Task CancelOrderWhichDoesNotExist()
        {
            var orderId = 1;

            var httpResponseMessage = await CancelOrder(orderId);

            httpResponseMessage.IsSuccessStatusCode.Should().BeFalse();
        }

        [Test]
        public async Task CancelOrder()
        {
            var customerId = 1;
            var bookIds = new List<int>() { 1 };

            var createOrderHttpResponseMessage = await CreateOrder(customerId, bookIds);
            createOrderHttpResponseMessage.EnsureSuccessStatusCode();

            var orderIdString = await createOrderHttpResponseMessage.Content.ReadAsStringAsync();

            if (!int.TryParse(orderIdString, out var orderId))
                throw new ArgumentOutOfRangeException(nameof(orderId));

            var cancelOrderGtHttpResponseMessage = await CancelOrder(orderId);
            cancelOrderGtHttpResponseMessage.IsSuccessStatusCode.Should().BeTrue();
            cancelOrderGtHttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.Accepted);
        }

        private async Task<HttpResponseMessage> CreateOrder(int customerId, List<int> bookIds)
        {
            var createOrderCommand = new CreateOrderCommand()
            {
                CustomerId = customerId,
                BookIds = bookIds
            };

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(createOrderCommand), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var createOrderHttpResponseMessage = await _client.PostAsync(CreateOrderUri, httpContent, default);

            return createOrderHttpResponseMessage;
        }

        private async Task<HttpResponseMessage> CancelOrder(int orderId)
        {
            var uri = $"/api/order/{orderId}/cancel";
            var cancelOrderHttpResponseMessage = await _client.PutAsync(uri, new StringContent(string.Empty), default);

            return cancelOrderHttpResponseMessage;
        }
    }
}