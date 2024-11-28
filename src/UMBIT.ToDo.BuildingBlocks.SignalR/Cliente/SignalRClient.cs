using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using TSE.Nexus.SDK.SignalR.Interfaces;
using TSE.Nexus.SDK.SignalR.Modelos;

namespace TSE.Nexus.SDK.SignalR.Cliente
{
    public class SignalRClient : ISignalRClient
    {
        private HubConnection Conexao;
        private ILogger<SignalRClient> Logger;
        public SignalRClient(IOptions<SignalClientSettings> options, ILogger<SignalRClient> logger)
        {
            Logger = logger;
            var url = $"{options.Value?.SignalURL}/{options.Value?.Hub ?? "hub"}";

            Logger.Log(LogLevel.Information, $"Conectando em servidor hub signalr '{url}'.");

            Conexao = new HubConnectionBuilder()
                .WithUrl(new Uri(url), t =>
                {
                    t.SkipNegotiation = true;
                    t.CloseTimeout = TimeSpan.FromSeconds(30);
                    t.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
                })
                .WithAutomaticReconnect()
                .Build();

            Conexao.Closed += async (error) =>
            {
                this.Logger.LogError(error, "Erro na Conexão!");

                await Task.Delay(new Random().Next(0, 5) * 1000);
                await Conexao.StartAsync();
            };
        }


        public void EmitaAtualizacao(string metodo, object dados, string grupo = null)
        {
            VerifiqueConnectClient();

            try
            {
                this.Logger.LogInformation($"Emitindo atualização via SignalR. {grupo}, {metodo}");
                this.Conexao?.InvokeAsync("Atualizar", grupo ?? metodo, metodo, dados).Wait();
            }
            catch (Exception ex)
            {
                this.Conexao?.StopAsync();

                this.Logger.LogError(ex, "Erro na emissão de atualização!");
            }
        }

        public void Inicializar()
        {
            Conexao.StartAsync().Wait();
        }

        public void RecebaAtualizacao<T>(string metodo, Action<T> handler, string grupo = null)
        {
            VerifiqueConnectClient();

            try
            {
                this.Logger.LogInformation("Configurando handler de atualização via SignalR");
                this.Conexao?.InvokeAsync("Registrar", grupo ?? metodo).Wait();
                this.Conexao?.On<T>(metodo, handler);
            }
            catch (Exception ex)
            {
                this.Conexao?.StopAsync();

                this.Logger.LogError(ex, "Erro na recepção de atualização!");
            }
        }

        private void VerifiqueConnectClient()
        {
            if (this.Conexao?.State != HubConnectionState.Connected)
            {
                this.Logger.LogInformation("Cliente Desconectado!");

                Conexao?.StartAsync().Wait();
            }
        }
    }
}
