using Microsoft.AspNetCore.Builder;
using UMBIT.ToDo.Fabrica.Models;

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
