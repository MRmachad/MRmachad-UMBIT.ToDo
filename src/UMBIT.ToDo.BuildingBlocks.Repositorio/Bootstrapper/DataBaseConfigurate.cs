using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UMBIT.ToDo.Core.Basicos.Utilitarios;
using UMBIT.ToDo.Core.Repositorio.EF;
using UMBIT.ToDo.Core.Repositorio.Interfaces.Database;

namespace UMBIT.ToDo.Core.Repositorio.Bootstrapper
{
    public static class DataBaseConfigurate
    {
        public static string CONNECTION_STRING_KEY = "default";

        public static IServiceCollection AddDataBase<T>(this IServiceCollection services, IConfiguration configuration) where T : BaseContext<T>
        {
            var connectString = configuration.GetConnectionString(CONNECTION_STRING_KEY);

            services.AddDbContext<DbContext, T>(options => options.UseSqlServer(connectString, b => b.MigrationsAssembly(ProjetoAssemblyHelper.NameProjetoInterface)));

            services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();
            services.AddScoped<IUnidadeDeTrabalhoDeLeitura, UnidadeDeTrabalhoDeLeitura>();
            services.AddScoped<IUnidadeDeTrabalhoNaoTransacional, UnidadeDeTrabalhoNaoTransacional>();

            return services;
        }
    }
}
