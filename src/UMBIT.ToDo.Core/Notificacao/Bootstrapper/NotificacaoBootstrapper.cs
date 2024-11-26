using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UMBIT.ToDo.Core.Notificacao;
using UMBIT.ToDo.Core.Notificacao.Interfaces;

namespace UMBIT.ToDo.Core.Notificacao.Bootstrapper
{
    public static class NotificacaoBootstrapper
    {
        public static IServiceCollection AdicionarNotificacao(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INotificador, Notificador>();

            return services;
        }
    }
}
