using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UMBIT.ToDo.Core.Basicos.Utilitarios;
using UMBIT.ToDo.Core.Repositorio.Basicos.Enumeradores;
using UMBIT.ToDo.Core.Repositorio.EF;
using UMBIT.ToDo.Core.Repositorio.Interfaces.Database;

namespace UMBIT.ToDo.Core.Repositorio.Bootstrapper
{
    public static class DataBaseConfigurate
    {
        public static string DATA_MODE_KEY = "Data_Mode";
        public static string CONNECTION_STRING_KEY = "default";

        public static IServiceCollection AddDataBase<T>(this IServiceCollection services, IConfiguration configuration) where T : BaseContext<T>
        {
            var connectString = configuration.GetConnectionString(CONNECTION_STRING_KEY);
            var dataBaseModeResult = Enum.TryParse(typeof(DataBaseEnum), configuration.GetSection(DATA_MODE_KEY).Value, out var dataModel);

            if (dataBaseModeResult)
            {
                var enumResult = (DataBaseEnum)dataModel!;
                switch (enumResult)
                {
                    case DataBaseEnum.Postgresql:
                        services.AddDbContext<DbContext, T>(options => options.UseNpgsql(connectString, b => b.MigrationsAssembly(ProjetoAssemblyHelper.NameProjetoInterface)));
                        break;
                    case DataBaseEnum.MySQL:
                        services.AddDbContext<DbContext, T>(options => options.UseMySql(connectString, ServerVersion.AutoDetect(connectString), b => b.MigrationsAssembly(ProjetoAssemblyHelper.NameProjetoInterface)));
                        break;
                    default:
                        services.AddDbContext<DbContext, T>(options => options.UseNpgsql(connectString, b => b.MigrationsAssembly(ProjetoAssemblyHelper.NameProjetoInterface)));
                        break;
                }
            }

            services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();
            services.AddScoped<IUnidadeDeTrabalhoDeLeitura, UnidadeDeTrabalhoDeLeitura>();
            services.AddScoped<IUnidadeDeTrabalhoNaoTransacional, UnidadeDeTrabalhoNaoTransacional>();

            return services;
        }
    }
}
