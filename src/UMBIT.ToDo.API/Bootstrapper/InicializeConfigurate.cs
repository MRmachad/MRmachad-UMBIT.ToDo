using UMBIT.ToDo.Core.Basicos.Utilitarios;

namespace TSE.Nexus.NodeLink.API.Bootstrapper
{
    public static class InicializeConfigurate
    {
        public static void Inicialize()
        {
            ProjetoAssemblyHelper.Inicialize(
                "UMBIT.ToDo.Dominio",
                "UMBIT.ToDo.Contrato",
                "UMBIT.ToDo.API",
                "UMBIT.ToDo.Infraestrutura");
        }
    }
}
