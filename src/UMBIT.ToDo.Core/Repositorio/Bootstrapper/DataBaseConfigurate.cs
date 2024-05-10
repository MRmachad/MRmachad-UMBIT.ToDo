﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UMBIT.MicroService.SDK.Repositorio.Basicos.Enumeradores;
using UMBIT.MicroService.SDK.Repositorio.EF;
using UMBIT.MicroService.SDK.Repositorio.Interfaces.Database;

namespace UMBIT.MicroService.SDK.Repositorio.Bootstrapper
{
    public static class DataBaseConfigurate
    {
        public static string DATA_MODE_KEY = "Data_Mode";
        public static string CONNECTION_STRING_KEY = "default";
        public static IServiceCollection AddDataBase<T>(this IServiceCollection services, IConfiguration configuration, string nomeServico) where T : BaseContext<T>
        {
            var connectString = configuration.GetConnectionString(CONNECTION_STRING_KEY);
            var dataBaseModeResult = Enum.TryParse(typeof(DataBaseEnum), configuration.GetSection(DATA_MODE_KEY).Value, out var dataMode);
            services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();

            if (dataBaseModeResult)
            {
                var enumResult = (DataBaseEnum)dataMode;
                switch (enumResult)
                {
                    case DataBaseEnum.Postgresql:
                        services.AddDbContext<DbContext, T>(options => options.UseNpgsql(connectString, b => b.MigrationsAssembly(nomeServico)));
                        break;
                    case DataBaseEnum.MySQL:
                        services.AddDbContext<DbContext, T>(options => options.UseMySql(connectString, ServerVersion.AutoDetect(connectString), b => b.MigrationsAssembly(nomeServico)));
                        break;
                    default:
                        services.AddDbContext<DbContext, T>(options => options.UseNpgsql(connectString, b => b.MigrationsAssembly(nomeServico)));
                        break;
                }
            }
            else
            {

            }

            return services;
        }
    }
}
