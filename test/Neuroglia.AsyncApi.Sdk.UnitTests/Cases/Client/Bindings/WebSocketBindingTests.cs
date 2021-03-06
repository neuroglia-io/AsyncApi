using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Modules;
using DotNet.Testcontainers.Containers.WaitStrategies;
using Neuroglia.AsyncApi.Models.Bindings.WebSockets;
using Neuroglia.AsyncApi.Services.FluentBuilders;
using Neuroglia.AsyncApi.UnitTests.Data;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Neuroglia.AsyncApi.Sdk.UnitTests.Cases.Client
{
    public class WebSocketBindingTests
        : BindingTestsBase
    {

        const int ContainerPort = 80;

        public WebSocketBindingTests()
            : base(ServerSetup)
        {

        }

        TestcontainersContainer Container { get; set; }

        protected override bool SupportsHeaders => false;

        protected override void Initialize()
        {
            var containerBuilder = new TestcontainersBuilder<TestcontainersContainer>()
                .WithImage("skandyla/go-websocket-echo-server")
                .WithName("ws-echo")
                .WithExposedPort(ContainerPort)
                .WithPortBinding(ContainerPort, ContainerPort)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(ContainerPort));
            this.Container = containerBuilder.Build();
            this.Container.StartAsync().GetAwaiter().GetResult();
            base.Initialize();
        }

        protected override void ConfigureCorrelationId(IRuntimeExpressionBuilder correlationId)
        {
            correlationId
                .In(RuntimeExpressionSource.Payload)
                .At(nameof(TestUser.Id));
        }

        [Fact(Skip = "There's a problem with the go-websocket-echo-server image. We should find another image to tests websockets")]
        public override Task SubscribeAndPublish()
        {
            return base.SubscribeAndPublish();
        }

        protected override async ValueTask DisposeAsync(bool disposing)
        {
            if (!disposing)
                return;
            await this.Container.StopAsync();
            await this.Container.DisposeAsync();
        }

        static void ServerSetup(IServerDefinitionBuilder server)
        {
            server.WithProtocol(AsyncApiProtocols.Ws)
                .WithUrl(new Uri($"ws://localhost:{ContainerPort}", UriKind.RelativeOrAbsolute))
                .UseBinding(new WsServerBindingDefinition());
        }

    }

}
