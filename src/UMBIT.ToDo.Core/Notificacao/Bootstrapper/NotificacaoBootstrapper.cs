using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UMBIT.MicroService.SDK.Notificacao.Interfaces;

namespace UMBIT.MicroService.SDK.Notificacao.Bootstrapper
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
