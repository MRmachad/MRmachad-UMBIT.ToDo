using System.Runtime.CompilerServices;

namespace UMBIT.ToDo.Core.Repositorio.Interfaces.Database
{
    public interface IUnidadeDeTrabalhoNaoTransacional : IDisposable
    {
        IRepositorio<T> ObterRepositorio<T>() where T : class;

        Task<int> SalveAlteracoes();

    }
}
