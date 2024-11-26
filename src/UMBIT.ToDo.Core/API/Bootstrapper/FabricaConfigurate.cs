using Microsoft.AspNetCore.Builder;
using UMBIT.ToDo.Core.API.Models.Fabrica;

namespace UMBIT.ToDo.Core.API.Bootstrapper
{
    public static class FabricaConfigurate
    {
        public static IApplicationBuilder UseFabricaGenerica(this IApplicationBuilder app)
        {
            FabricaGenerica.Initialize(app.ApplicationServices);
            return app;
        }
    }
}
