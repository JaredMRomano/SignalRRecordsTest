using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using SignalRRecordsTest.Hubs;
using SignalRRecordsTest.Requests;
using System.Threading.Tasks;
using System;
using System.Text.Json;

namespace SignalRRecordsTest
{
    public class Tests
    {
        TestServer server;
        private const int value = 1;
        private const string message = "Integration Testing in Microsoft AspNetCore SignalR";
        [SetUp]
        public async Task Setup()
        {
            var webHostBuilder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSignalR()
                    .AddJsonProtocol(o => o.PayloadSerializerOptions.PropertyNameCaseInsensitive = true);
                })
                .Configure(app =>
                {
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapHub<RecordsTestHub>("/test");
                    });
                });

            server = new TestServer(webHostBuilder);
        }

        [Test]
        public async Task SerializeStandardClassRequest_Test()
        {
            StandardClassRequest test = new StandardClassRequest(1, "test");
            string json = JsonSerializer.Serialize(test);
            var newRequest = JsonSerializer.Deserialize<StandardClassRequest>(json);
            Assert.Pass();
        }

        [Test]
        public async Task SerializeImmutableClassRequest_Test()
        {
            ImmutableClassRequest test = new ImmutableClassRequest(1, "test");
            string json = JsonSerializer.Serialize(test);
            var newRequest = JsonSerializer.Deserialize<StandardClassRequest>(json);
            Assert.Pass();
        }

        [Test]
        public async Task SerializeRecordRequest_Test()
        {
            PositionalRecordRequest test = new PositionalRecordRequest(1, "test");
            string json = JsonSerializer.Serialize(test);
            var newRequest = JsonSerializer.Deserialize<StandardClassRequest>(json);
            Assert.Pass();
        }

        [Test]
        public async Task SendStandardClassRequest_Test()
        {
            var echo = string.Empty;

            var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost/test", o => o.HttpMessageHandlerFactory = _ => server.CreateHandler())
            .Build();

            connection.On<string>("OnMessageRecieved", msg =>
            {
                echo = msg;
            });

            StandardClassRequest request = new StandardClassRequest(1, "test");

            await connection.StartAsync();

            await connection.InvokeAsync("SendStandardClassRequest", request);

            await Task.Delay(200);

            Assert.AreEqual(request.StringProp, echo);            
        }

        [Test]
        public async Task SendRecordRequest_Test()
        {
            var echo = string.Empty;
            var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost/test", o => o.HttpMessageHandlerFactory = _ => server.CreateHandler())
            .Build();

            connection.On<string>("OnMessageRecieved", msg =>
            {
                echo = msg;
            });

            PositionalRecordRequest request = new PositionalRecordRequest(1, "test");

            await connection.StartAsync();

            await connection.InvokeAsync("SendRecordRequest", message);

            await Task.Delay(200);

            Assert.AreEqual(request.StringProp, echo);
        }

        [Test]
        public async Task SendImmutableClassRequest_Test()
        {
            var echo = string.Empty;

            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost/test", o => o.HttpMessageHandlerFactory = _ => server.CreateHandler())
                .Build();

            connection.On<string>("OnMessageRecieved", msg =>
            {
                echo = msg;
            });

            ImmutableClassRequest request = new ImmutableClassRequest(1, "test");

            await connection.StartAsync();
            await connection.InvokeAsync("SendImmutableClassRequest", message);
            await Task.Delay(200);

            Assert.AreEqual(request.StringProp, echo);
        }
    }
}