using Microsoft.Extensions.DependencyInjection;

namespace UMBIT.ToDo.Fabrica.Models
{
    public static class FabricaGenerica
    {
        private static IServiceProvider _serviceProvider;
        public static IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider.CreateScope().ServiceProvider;
            }
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
    }
}
