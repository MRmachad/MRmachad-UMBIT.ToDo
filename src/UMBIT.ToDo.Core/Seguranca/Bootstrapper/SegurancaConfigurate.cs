using Microsoft.Extensions.DependencyInjection;
using UMBIT.ToDo.Core.Seguranca.Models;

namespace UMBIT.ToDo.Core.Seguranca.Bootstrapper
{
    public static class SegurancaConfigurate
    {
        public static IServiceCollection AddContextPrincipal(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ContextoPrincipal>();

            return services;
        }
    }
}
