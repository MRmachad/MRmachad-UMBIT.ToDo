using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UMBIT.ToDo.SDK.Notificacao.Interfaces;

namespace UMBIT.ToDo.SDK.Notificacao.Bootstrapper
{
    public static class NotificacaoBootstrapper
    {
        public static IServiceCollection AdicionarNotificacao(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();

            return services;
        }
    }
}
