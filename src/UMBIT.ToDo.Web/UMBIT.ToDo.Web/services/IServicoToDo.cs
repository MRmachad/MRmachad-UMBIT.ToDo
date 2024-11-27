using Refit;
using UMBIT.Nexus.Auth.Contrato;

namespace UMBIT.ToDo.Web.services
{
    public interface IServicoToDo
    {
        [Post("/tarefa")]
        Task AdicioneItem([Body] AdicionarTarefaRequest request);

        [Put("/tarefa")]
        Task EditItem([Body] AtualizarTarefaRequest request);

        [Delete("/tarefa/{id}")]
        Task DeleteItem([Refit.AliasAs("id")] Guid id);

        [Get("/tarefa/{id}")]
        Task<TarefaDTO> ObtenhaItem([Refit.AliasAs("id")] Guid id);

        [Get("/tarefa?$filter=contains(cast(status,'Edm.String'), '{status}') AND contains(cast(idToDoList,'Edm.String'), '{idToDoList}')")]
        Task<List<TarefaDTO>?> ObtenhaItens([AliasAs("status")] int? status, [Refit.AliasAs("idToDoList")] Guid? idToDoList);

        [Post("/lista-tarefa")]
        Task AdicioneList([Body] AdicionarListaRequest request);

        [Put("/lista-tarefa")]
        Task EditList([Body] AtualizarListaRequest request);

        [Delete("/lista-tarefa/{id}")]
        Task DeleteList([Refit.AliasAs("id")] Guid id);

        [Get("/lista-tarefa/{id}")]
        Task<ListaDTO> ObtenhaList([Refit.AliasAs("id")] Guid id);

        [Get("/lista-tarefa")]
        Task<List<ListaDTO>?> ObtenhaLists();
    }
}
