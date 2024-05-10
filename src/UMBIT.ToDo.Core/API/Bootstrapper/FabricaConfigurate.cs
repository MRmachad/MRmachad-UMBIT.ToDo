using Microsoft.Extensions.DependencyInjection;
using UMBIT.ToDo.SDK.API.Models.Fabrica;

namespace UMBIT.ToDo.SDK.API.Bootstrapper
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
