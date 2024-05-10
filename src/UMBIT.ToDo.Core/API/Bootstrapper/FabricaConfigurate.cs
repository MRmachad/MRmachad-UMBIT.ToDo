using Microsoft.Extensions.DependencyInjection;
using UMBIT.MicroService.SDK.API.Models.Fabrica;

namespace UMBIT.MicroService.SDK.API.Bootstrapper
{
    public static class FabricaConfigurate
    {
        public static IServiceCollection AddFabricaGenerica(this IServiceCollection services)
        {
            FabricaGenerica.services = services.BuildServiceProvider();
            return services;
        }
    }
}
