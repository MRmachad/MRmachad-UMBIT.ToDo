using UMBIT.ToDo.Dominio.Interfaces;
using UMBIT.ToDo.Dominio.Servicos;

namespace UMBIT.ToDo.API.Bootstrapper
{
    public static class DependenciasConfigurate
    {
        public static IServiceCollection AddDependencias(this IServiceCollection services)
        {
            services.AddScoped<IServicoDeToDo, ServicoDeToDo>();
            return services;
        }
    }
}
