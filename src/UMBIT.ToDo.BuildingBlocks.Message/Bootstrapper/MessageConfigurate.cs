
using System.Reflection;
using TSE.Nexus.SDK.Messages.Bus.MessagerBus.Models;
using TSE.Nexus.SDK.Messages.Bus.MessagerBus;
using UMBIT.ToDo.Core.Basicos.Utilitarios;
using UMBIT.ToDo.Core.Messages.Bus.MediatorBus;
using UMBIT.ToDo.Core.Repositorio.Interfaces.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace UMBIT.ToDo.Core.Messages.Bootstrapper
{
    public static class MessageConfigurate
    {
        public static IServiceCollection AddMessages(this IServiceCollection services, IConfiguration configuration, List<Assembly>? CurtomRegisterAssemblies = null)
        {
            var appAssemblies = ProjetoAssemblyHelper.ObtenhaAppAssemblys().Append(typeof(MessageConfigurate).Assembly).Append(typeof(IUnidadeDeTrabalho).Assembly)!;

            appAssemblies = CurtomRegisterAssemblies == null ? appAssemblies : appAssemblies.Concat(CurtomRegisterAssemblies);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(appAssemblies.ToArray()));
            services.AddTransient<IMediatorBus, MediatorBus>();

            services.Configure<TSEMessageBusConfig>((t) =>
            {
                t = JsonSerializer.Deserialize<TSEMessageBusConfig>(configuration.GetSection(nameof(TSEMessageBusConfig)).Value) ?? new TSEMessageBusConfig();
            });

            services.AddSingleton<TSEMessageBusConnectionFactory>();
            services.AddSingleton<IMessagerBus, MessagerBus>();

            return services;
        }
    }
}
