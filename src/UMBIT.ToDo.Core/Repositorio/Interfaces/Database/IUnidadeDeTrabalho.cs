using System.Runtime.CompilerServices;

namespace UMBIT.ToDo.Core.Repositorio.Interfaces.Database
{
    public interface IUnidadeDeTrabalho : IUnidadeDeTrabalhoNaoTransacional
    {
        Task InicieTransacao();

        Task FinalizeTransacao();

        Task RevertaTransacao();
    }

}
