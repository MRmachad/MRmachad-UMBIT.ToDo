using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UMBIT.ToDo.Core.API.Models.Fabrica;

namespace UMBIT.ToDo.Core.API.Bootstrapper
{
    public static class FabricaConfigurate
    {
        public static IApplicationBuilder AddFabricaGenerica(this IApplicationBuilder app)
        {
            FabricaGenerica.Initialize(app.ApplicationServices);
            return app;
        }
    }
}
