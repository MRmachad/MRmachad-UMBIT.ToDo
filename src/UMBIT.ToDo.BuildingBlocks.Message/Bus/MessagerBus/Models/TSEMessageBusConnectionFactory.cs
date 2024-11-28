using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace TSE.Nexus.SDK.Messages.Bus.MessagerBus.Models
{
    public class TSEMessageBusConnectionFactory
    {
        public ConnectionFactory ConnectionFactory { get; private set; }
        public TSEMessageBusConnectionFactory(IOptions<TSEMessageBusConfig> messageBusConfig)
        {
            if(this.ConnectionFactory == null)
            {
                this.ConnectionFactory = new ConnectionFactory()
                {
                    HostName = messageBusConfig?.Value?.Host ?? "localhost",
                    Password = messageBusConfig?.Value?.Senha ?? "guest",
                    UserName = messageBusConfig?.Value?.Usuario ?? "guest",
                    Port = messageBusConfig?.Value?.Port ?? 5672
                };

                this.ConnectionFactory.AutomaticRecoveryEnabled = true;
                this.ConnectionFactory.RequestedHeartbeat = TimeSpan.FromSeconds(60);
                this.ConnectionFactory.NetworkRecoveryInterval = TimeSpan.FromSeconds(10);
            }
        }

        public string ObtenhaURL() => $"amqp://{this.ConnectionFactory.HostName}:{this.ConnectionFactory.Port}"  ?? String.Empty;
    }
}
