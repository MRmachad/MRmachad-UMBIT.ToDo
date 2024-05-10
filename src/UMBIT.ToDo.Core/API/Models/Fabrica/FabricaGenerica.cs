using Microsoft.Extensions.DependencyInjection;
using UMBIT.ToDo.SDK.Basicos.Excecoes;

namespace UMBIT.ToDo.SDK.API.Models.Fabrica
{
    public static class FabricaGenerica
    {
        public static IServiceProvider services { get; set; }

        public static T Crie<T>()
        {
            try
            {
                return services.GetService<T>();
            }
            catch (Exception ex)
            {
                throw new ExcecaoBasicaUMBIT($"Erro ao resgatar serviço {nameof(T)}", ex);
            }
        }
    }
}
