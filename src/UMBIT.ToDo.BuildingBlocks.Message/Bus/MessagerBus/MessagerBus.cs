using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;
using TSE.Nexus.SDK.Messages.Bus.MessagerBus.Models;
using UMBIT.ToDo.Core.Basicos.Notificacoes;
using UMBIT.ToDo.Core.Messages.Messagem;

namespace TSE.Nexus.SDK.Messages.Bus.MessagerBus
{
    internal class MessagerBus : IMessagerBus
    {
        private const string TOPIC_EXCHANGE = "TOPIC_EXCHANGE";

        private JsonSerializerOptions JsonSerializerOptions;
        private List<string> RPCListeners = new List<string>();
        private bool IsConnected => this.Connection?.IsOpen ?? false;
        private TSEMessageBusConnectionFactory TSEMessageBusConnectionFactory;

        internal readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _activeTaskQueue = new ConcurrentDictionary<string, TaskCompletionSource<string>>();

        internal IModel Channel;
        internal IConnection Connection;
        internal ILogger<MessagerBus> Logger;

        public MessagerBus(TSEMessageBusConnectionFactory tseMessageBusConnectionFactory, ILogger<MessagerBus> logger)
        {
            this.Logger = logger;
            this.TSEMessageBusConnectionFactory = tseMessageBusConnectionFactory;

            this.JsonSerializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
            };

            try
            {
                TryConnect();
            }
            catch (Exception ex)
            {
            }
        }

        public bool EhValido()
        {
            return this.IsConnected;
        }

        public void Publish<T>(T message, string nomeQueue = "null", string exchange = "") where T : UMBITMensagem, new()
        {
            TryConnect();

            nomeQueue = !String.IsNullOrEmpty(nomeQueue) ? nomeQueue : typeof(T).Namespace!;
            exchange = !String.IsNullOrEmpty(exchange) ? exchange : nomeQueue;

            this.Channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);

            var jsonMessage = JsonSerializer.SerializeToUtf8Bytes(message, JsonSerializerOptions);

            this.Channel.BasicPublish(
                mandatory: true,
                routingKey: "",
                exchange: exchange,
                basicProperties: null,
                body: jsonMessage);
        }
        public void Subscribe<T>(Action<T> onMessage, string nomeQueue = null, string exchange = "") where T : UMBITMensagem, new()
        {
            TryConnect();

            nomeQueue = !String.IsNullOrEmpty(nomeQueue) ? nomeQueue : typeof(T).Namespace!;
            exchange = !String.IsNullOrEmpty(exchange) ? exchange : nomeQueue;

            this.Channel.QueueDeclare(nomeQueue, durable: true, false, false, null);

            if (!String.IsNullOrEmpty(exchange))
            {
                this.Channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);

                this.Channel.QueueBind(queue: nomeQueue,
                                       exchange: exchange,
                                       routingKey: string.Empty);

            }


            var consumer = new EventingBasicConsumer(this.Channel);
            consumer.Received += async (bc, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

                try
                {
                    var data = JsonSerializer.Deserialize<T>(message, JsonSerializerOptions);
                    onMessage(data);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Erro na descearialização de mensagem");
                }

                await Task.CompletedTask;
            };

            this.Channel.BasicConsume(queue: nomeQueue, true, consumer);
        }
        private bool TryConnect()
        {
            this.Logger.Log(LogLevel.Information, $"Conectando ao servidor de mensageria '{TSEMessageBusConnectionFactory.ObtenhaURL()}'.");

            var factory = this.TSEMessageBusConnectionFactory.ConnectionFactory;

            if (IsConnected) return true;
            if (factory == null) return false;

            this.Connection = factory.CreateConnection();
            this.Channel = this.Connection.CreateModel();

            this.Connection.ConnectionShutdown += (connection, evt) =>
            {
                Logger.LogError("Conexão TSEMessageBus finalizada");
            };

            return EhValido();
        }

    }
}
