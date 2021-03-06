using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Neuroglia.AsyncApi.Client;
using Neuroglia.AsyncApi.Client.Services;
using Neuroglia.AsyncApi.Models;
using Neuroglia.AsyncApi.Services.FluentBuilders;
using Neuroglia.AsyncApi.UnitTests.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Neuroglia.AsyncApi.Sdk.UnitTests.Cases.Client
{

    public abstract class BindingTestsBase
        : IDisposable, IAsyncDisposable
    {

        protected const string ChannelKey = "light/measured";
#pragma warning disable CA2211 // Non-constant fields should not be visible
        protected static string ClientId = Guid.NewGuid().ToString();
#pragma warning restore CA2211 // Non-constant fields should not be visible

        protected BindingTestsBase(Action<IServerDefinitionBuilder> serverSetup)
        {
            this.Initialize();
            var services = new ServiceCollection();
            services = new ServiceCollection();
            services.AddAsyncApiClientFactory(builder =>
                builder.UseAllBindings());
            var document = this.BuildDocument(serverSetup);
            this.AsyncApiClient = services.BuildServiceProvider().GetRequiredService<IAsyncApiClientFactory>().CreateClient(document);
        }

        protected IAsyncApiClient AsyncApiClient { get; }

        protected virtual bool SupportsHeaders => true;

        protected virtual void Initialize()
        {

        }

        [Fact]
        public virtual async Task SubscribeAndPublish()
        {
            //arrange
            var channelKey = ChannelKey;
            CancellationTokenSource cancellationTokenSource = null;
            IMessage consumedMessage = null;
            IDisposable subscription = null;
            var observer = Observer.Create<IMessage>(message =>
            {
                consumedMessage = message;
                cancellationTokenSource.Cancel();
            });
            var producedMessage = new MessageBuilder()
                .WithPayload(new TestUser() { FirstName = "Fake First Name", LastName = "Fake Last Name" })
                .WithHeader("Fake Header 1", "Fake Value 1")
                .WithHeader("Fake Header 2", "Fake Value 2")
                .WithCorrelationKey(Guid.NewGuid())
                .Build();

            //act
            subscription = await this.AsyncApiClient.SubscribeToAsync(channelKey, observer);
            await this.AsyncApiClient.PublishAsync(channelKey, producedMessage);
            cancellationTokenSource = new(5000);
            while (!cancellationTokenSource.IsCancellationRequested) { }

            //assert
            consumedMessage.Should().NotBeNull();
            consumedMessage.Payload.As<JObject>().ToObject<TestUser>().Should().BeEquivalentTo(producedMessage.Payload);

            if (this.SupportsHeaders)
            {
                consumedMessage.Headers.ElementAt(0).Key.Should().Be(producedMessage.Headers.ElementAt(0).Key);
                consumedMessage.Headers.ElementAt(0).Value.As<JToken>().ToString().Should().Be(producedMessage.Headers.ElementAt(0).Value as string);

                consumedMessage.Headers.ElementAt(1).Key.Should().Be(producedMessage.Headers.ElementAt(1).Key);
                consumedMessage.Headers.ElementAt(1).Value.As<JToken>().ToString().Should().Be(producedMessage.Headers.ElementAt(1).Value as string);
            }

            consumedMessage.CorrelationKey.Should().NotBeNull();
            consumedMessage.CorrelationKey.As<JToken>().ToObject<Guid>().Should().Be((Guid)producedMessage.CorrelationKey);

            Func<Task> act = () => this.AsyncApiClient.PublishAsync(ChannelKey, message => message.WithPayload(new { Message = "This should fail" }));
            await act.Should().ThrowAsync<FormatException>();
        }

        private bool _Disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!this._Disposed)
            {
                if (disposing)
                {
                    this.DisposeAsync().GetAwaiter().GetResult(); //todo: that is bad, really bad. Should replace once XUnit supports IAsyncDisposable
                }
                this._Disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual ValueTask DisposeAsync(bool disposing)
        {
            if (!this._Disposed)
            {
                if (disposing)
                {

                }
                this._Disposed = true;
            }
            return ValueTask.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            await this.DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual AsyncApiDocument BuildDocument(Action<IServerDefinitionBuilder> serverSetup)
        {
            var services = new ServiceCollection();
            services.AddAsyncApi();
            var documentBuilder = services.BuildServiceProvider().GetRequiredService<IAsyncApiDocumentBuilder>();
            return documentBuilder
                .WithId("Fake API")
                .WithVersion("1.0.0")
                .UseServer("Fake Server", server => serverSetup(server))
                .UseChannel(ChannelKey, ConfigureChannel)
                .Build();
        }

        protected virtual void ConfigureChannel(IChannelDefinitionBuilder channel)
        {
            channel
                .DefineSubscribeOperation(ConfigureSubscribeOperation)
                .DefinePublishOperation(ConfigurePublishOperation);
        }

        protected virtual void ConfigureSubscribeOperation(IOperationDefinitionBuilder operation)
        {
            operation
                .WithOperationId("Fake Subscribe")
                .UseMessage(this.ConfigureMessage);
        }

        protected virtual void ConfigurePublishOperation(IOperationDefinitionBuilder operation)
        {
            operation
                .WithOperationId("Fake Publish")
                .UseMessage(this.ConfigureMessage);
        }

        protected virtual void ConfigureMessage(IMessageDefinitionBuilder message)
        {
            message
                .OfType<TestUser>()
                .WithCorrelationId(ConfigureCorrelationId);
        }

        protected virtual void ConfigureCorrelationId(IRuntimeExpressionBuilder correlationId)
        {
            correlationId
                .In(RuntimeExpressionSource.Header)
                .At("correlation-id");
        }

    }

}
