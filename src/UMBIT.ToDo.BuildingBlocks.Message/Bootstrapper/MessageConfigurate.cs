
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TSE.Nexus.SDK.Messages.Bus.MessagerBus;
using TSE.Nexus.SDK.Messages.Bus.MessagerBus.Models;
using UMBIT.ToDo.Core.Basicos.Utilitarios;
using UMBIT.ToDo.Core.Messages.Bus.MediatorBus;
using UMBIT.ToDo.Core.Repositorio.Interfaces.Database;

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

            services.Configure<TSEMessageBusConfig>(configuration.GetSection("TSEMessageBusConfig"));


            services.AddSingleton<TSEMessageBusConnectionFactory>();
            services.AddSingleton<IMessagerBus, MessagerBus>();

            return services;
        }
    }
}
