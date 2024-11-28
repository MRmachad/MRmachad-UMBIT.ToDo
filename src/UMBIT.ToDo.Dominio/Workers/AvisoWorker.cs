using Microsoft.Extensions.DependencyInjection;
using TSE.Nexus.SDK.Messages.Bus.MessagerBus;
using TSE.Nexus.SDK.SignalR.Interfaces;
using TSE.Nexus.SDK.Workers.Workers.JustFire;
using UMBIT.ToDo.Core.Messages.Messagem;

namespace UMBIT.ToDo.Dominio.Workers
{
    public class AvisoWorker : JustFireBaseWorker
    {
        private readonly IMessagerBus _messagerBus;
        public AvisoWorker(IServiceScopeFactory serviceScopeFactory, IMessagerBus messagerBus) : base(serviceScopeFactory)
        {
            _messagerBus = messagerBus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messagerBus.Subscribe<AvisoMessage>((t) =>
            {
                var signalClient = ServiceScopeFactory.CreateScope().ServiceProvider.GetService<ISignalRClient>();

                signalClient.EmitaAtualizacao("avisos", "Faça sua Tarefa", t.IdUsuario.ToString());

            });

            return Task.CompletedTask;
        }

        public class AvisoMessage : UMBITMensagem
        {
            public AvisoMessage()
            {

            }
            public AvisoMessage(Guid idUsuario)
            {
                IdUsuario = idUsuario;
            }

            public Guid IdUsuario { get; set; }

        }
    }
}
